using Legends.Core.IO.Inibin;
using Legends.Core.IO.RAF;
using Legends.Core.Utils;
using Legends.DatabaseSynchronizer.Attributes;
using Legends.ORM;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using Legends.ORM.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer
{
    /// <summary>
    /// https://github.com/moonshadow565/LoL-1.0.0.673-draft/tree/17eeb91cf18eb9920027706590bbe5e70af2bcfb/draft
    /// </summary>
    public class InibinSynchronizer
    {
        private Assembly RecordAssembly
        {
            get;
            set;
        }
        private string LeagueOfLegendsPath
        {
            get;
            set;
        }
        private RafManager RafManager
        {
            get;
            set;
        }
        private Logger logger = new Logger();

        public InibinSynchronizer(string leagueOfLegendsPath, Assembly recordAssembly)
        {
            this.RecordAssembly = recordAssembly;
            this.LeagueOfLegendsPath = leagueOfLegendsPath;
            this.RafManager = new RafManager(leagueOfLegendsPath);
        }
        public ITable[] GetRecords(Type type, RAFFileEntry[] entries)
        {
            List<ITable> records = new List<ITable>();

            foreach (var entry in entries)
            {
                var inibin = new InibinFile(new MemoryStream(entry.GetContent(true)));

                var record = (ITable)Activator.CreateInstance(type);

                foreach (var field in record.GetType().GetFields())
                {
                    var attribute = field.GetCustomAttribute<InibinFieldAttribute>();

                    if (attribute != null)
                    {
                        foreach (InibinFlags flag in Enum.GetValues(typeof(InibinFlags)))
                        {
                            if (inibin.Sets.ContainsKey(flag))
                            {
                                if (inibin.Sets[flag].Properties.ContainsKey((uint)attribute.hash))
                                {
                                    var value = inibin.Sets[flag].Properties[(uint)attribute.hash];

                                    if (value.ToString() == "No")
                                    {
                                        field.SetValue(record, false);
                                    }
                                    else if (value.ToString() == "Yes")
                                    {
                                        field.SetValue(record, true);
                                    }
                                    else if (value.ToString() == true.ToString() && field.FieldType != typeof(Boolean))
                                    {
                                        field.SetValue(record, 1);
                                    }
                                    else if (value.ToString() == false.ToString() && field.FieldType != typeof(Boolean))
                                    {
                                        field.SetValue(record, 0);
                                    }
                                    else if (value.ToString().Split('.').Last() == "0")
                                    {
                                        field.SetValue(record, Convert.ChangeType(value.ToString().Split('.')[0], field.FieldType));
                                    }
                                    else if (value.ToString().Contains('.'))
                                    {
                                        value = value.ToString().Replace('.', ',');
                                        field.SetValue(record, Convert.ChangeType(value.ToString(), field.FieldType));
                                    }
                                    else if (value.ToString().StartsWith("."))
                                    {
                                        value = "0" + value.ToString().Replace('.', ',');
                                        field.SetValue(record, Convert.ChangeType(value.ToString(), field.FieldType));
                                    }
                                    else
                                    {
                                        try
                                        {
                                            field.SetValue(record, Convert.ChangeType(value.ToString(), field.FieldType));
                                        }
                                        catch
                                        {
                                            logger.Write("Unable to assign field (" + field.Name + ") to value :" + value + " for " + Path.GetFileNameWithoutExtension(entry.Path), MessageState.WARNING);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    var attribute2 = field.GetCustomAttribute<InibinFieldFileName>();

                    if (attribute2 != null)
                    {
                        field.SetValue(record, Path.GetFileNameWithoutExtension(entry.Path));
                    }
                }
                records.Add(record);
            }
            return records.ToArray();
        }
        public void Sync()
        {
            foreach (var type in Array.FindAll(RecordAssembly.GetTypes(), x => x.GetCustomAttribute<TableAttribute>() != null))
            {
                var hook = GetInibinMethodInfo(type);

                if (hook == null)
                {
                    continue;
                }
                RAFFileEntry[] entries = (RAFFileEntry[])hook.Invoke(null, new object[] { RafManager });

                ITable[] records = GetRecords(type, entries);

                DatabaseManager.GetInstance().CreateTable(type);
                DatabaseManager.GetInstance().WriterInstance(type, DatabaseAction.Add, records);
                logger.Write("Synchronized: " + type.Name);

            }
        }
        public MethodInfo GetInibinMethodInfo(Type recordType)
        {
            foreach (var method in typeof(InibinHook).GetMethods())
            {
                var attribute = method.GetCustomAttribute<InibinMethod>();

                if (attribute != null && attribute.recordType == recordType)
                {
                    return method;
                }
            }
            return null;
        }
    }
}
