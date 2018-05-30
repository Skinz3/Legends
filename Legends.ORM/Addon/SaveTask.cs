
using Legends;
using Legends.Core;
using Legends.Core.Utils;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using Legends.ORM.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;

namespace Legends.ORM.Addon
{
    public static class SaveTask
    {
        public static event Action OnSaveStarted;

        public delegate void OnSaveEndedDel(int elapsed);

        public static event OnSaveEndedDel OnSaveEnded;

        private static Timer _timer;

        private static Dictionary<Type, List<ITable>> _newElements = new Dictionary<Type, List<ITable>>();
        private static Dictionary<Type, List<ITable>> _updateElements = new Dictionary<Type, List<ITable>>();
        private static Dictionary<Type, List<ITable>> _removeElements = new Dictionary<Type, List<ITable>>();

        static bool Saving;

        static Logger logger = new Logger();

        public static void Initialize(int seconds)
        {
            _timer = new Timer(seconds * 1000);
            _timer.Elapsed += _timer_Elapsed;
            _timer.AutoReset = true;
            _timer.Start();


        }

        public static void AddElement(ITable element, bool addtolist = true)
        {
            lock (_newElements)
            {
                if (_newElements.ContainsKey(element.GetType()))
                {
                    if (!_newElements[element.GetType()].Contains(element))
                        _newElements[element.GetType()].Add(element);
                }
                else
                {
                    _newElements.Add(element.GetType(), new List<ITable> { element });
                }
            }
            if (addtolist)
            {
                AddToList(element);
            }
        }

        public static void AddToList(ITable element)
        {
            #region Add value into array
            var field = GetCache(element);
            if (field == null)
            {
                logger.Write("Unable to add record value to the list, static list field wasnt finded", MessageState.ERROR);
                return;
            }

            var method = field.FieldType.GetMethod("Add");
            if (method == null)
            {
                Console.WriteLine("Unable to add record value to the list, add method wasnt finded");
                return;
            }

            method.Invoke(field.GetValue(null), new object[] { element });
            #endregion
        }
        public static void UpdateElement(ITable element)
        {
            lock (_updateElements)
            {
                if (_newElements.ContainsKey(element.GetType()) && _newElements[element.GetType()].Contains(element))
                    return;

                if (_updateElements.ContainsKey(element.GetType()))
                {
                    if (!_updateElements[element.GetType()].Contains(element))
                        _updateElements[element.GetType()].Add(element);
                }
                else
                {
                    _updateElements.Add(element.GetType(), new List<ITable> { element });
                }
            }
        }

        public static void RemoveElement(ITable element, bool removefromlist = true)
        {
            if (element == null)
                return;
            lock (_removeElements)
            {
                if (_newElements.ContainsKey(element.GetType()) && _newElements[element.GetType()].Contains(element))
                {
                    RemoveFromList(element);
                    _newElements[element.GetType()].Remove(element);
                    return;
                }

                if (_updateElements.ContainsKey(element.GetType()) && _updateElements[element.GetType()].Contains(element))
                    _updateElements[element.GetType()].Remove(element);

                if (_removeElements.ContainsKey(element.GetType()))
                {
                    if (!_removeElements[element.GetType()].Contains(element))
                        _removeElements[element.GetType()].Add(element);
                }
                else
                {
                    _removeElements.Add(element.GetType(), new List<ITable> { element });
                }
            }
            if (removefromlist)
            {
                RemoveFromList(element);
            }
        }
        public static void RemoveFromList(ITable element)
        {
            var field = GetCache(element);
            if (field == null)
            {
                logger.Write("[Remove] Erreur ! Field unknown", MessageState.WARNING);
                return;
            }

            var method = field.FieldType.GetMethod("Remove");
            if (method == null)
            {
                logger.Write("[Remove] Erreur ! Field unknown", MessageState.INFO);
                return;
            }

            method.Invoke(field.GetValue(null), new object[] { element });
        }
        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Save();
        }


        public static void Save()
        {
            Saving = true;
            Stopwatch w = Stopwatch.StartNew();
            if (OnSaveStarted != null)
                OnSaveStarted();
            _timer.Stop();
            try
            {

                var types = _removeElements.Keys.ToList();
                foreach (var type in types)
                {
                    List<ITable> elements;
                    lock (_removeElements)
                        elements = _removeElements[type];

                    try
                    {
                        DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Remove, elements.ToArray());
                    }
                    catch (Exception e)
                    {
                        logger.Write(e.Message, MessageState.ERROR);
                    }

                    lock (_removeElements)
                        _removeElements[type] = _removeElements[type].Skip(elements.Count).ToList();

                }

                types = _newElements.Keys.ToList();
                foreach (var type in types)
                {
                    List<ITable> elements;

                    lock (_newElements)
                        elements = _newElements[type];

                    try
                    {
                        DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Add, elements.ToArray());
                    }
                    catch (Exception e)
                    {
                        logger.Write(e.Message, MessageState.ERROR);
                    }

                    lock (_newElements)
                        _newElements[type] = _newElements[type].Skip(elements.Count).ToList();

                }

                types = _updateElements.Keys.ToList();
                foreach (var type in types)
                {
                    List<ITable> elements;
                    lock (_updateElements)
                        elements = _updateElements[type];

                    try
                    {
                        DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Update, elements.ToArray());
                    }
                    catch (Exception e)
                    {
                        logger.Write(e.Message, MessageState.ERROR);
                    }

                    lock (_updateElements)
                    {
                        _updateElements[type] = _updateElements[type].Skip(elements.Count).ToList();
                    }
                }


                _timer.Start();
                if (OnSaveEnded != null)
                    OnSaveEnded(w.Elapsed.Seconds);

            }
            catch (Exception e)
            {
                logger.Write(e.Message, MessageState.ERROR);
            }
            finally
            {
                Saving = false;
            }
        }

        public static FieldInfo GetCache(Type type)
        {
            var attribute = type.GetCustomAttribute(typeof(TableAttribute), false);
            if (attribute == null)
                return null;

            var field = type.GetFields().FirstOrDefault(x => x.Name.ToLower() == (attribute as TableAttribute).tableName.ToLower());
            if (field == null || !field.IsStatic || !field.FieldType.IsGenericType)
                return null;

            return field;
        }
        private static FieldInfo GetCache(ITable table)
        {
            return GetCache(table.GetType());
        }
    }
}
