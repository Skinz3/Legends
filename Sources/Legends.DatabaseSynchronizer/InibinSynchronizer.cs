using Legends.Core.Attributes;
using Legends.Core.IO.Inibin;
using Legends.Core.IO.RAF;
using Legends.Core.Utils;
using Legends.DatabaseSynchronizer.Attributes;
using Legends.DatabaseSynchronizer.CustomSyncs;
using Legends.Records;
using SmartORM;
using SmartORM.Attributes;
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
                foreach (var property in type.GetProperties())
                {
                    var attribute2 = property.GetCustomAttribute<InibinFieldFileName>();

                    if (attribute2 != null)
                    {
                        property.SetValue(record, Convert.ChangeType(Path.GetFileNameWithoutExtension(entry.Path), property.PropertyType));
                    }

                    var attribute = property.GetCustomAttribute<InibinFieldAttribute>();

                    if (attribute != null)
                    {
                        foreach (InibinFlags flag in Enum.GetValues(typeof(InibinFlags)))
                        {
                            if (inibin.Sets.ContainsKey(flag))
                            {
                                if (inibin.Sets[flag].Properties.ContainsKey((uint)attribute.hash))
                                {
                                    var value = inibin.Sets[flag].Properties[(uint)attribute.hash];
                                    value = FieldSanitizer.Sanitize(value.ToString(), property.PropertyType);

                                    try
                                    {
                                        property.SetValue(record, Convert.ChangeType(value.ToString(), property.PropertyType));
                                    }
                                    catch
                                    {
                                        logger.Write("Unable to assign field (" + property.Name + ") to value :" + value + " for " + Path.GetFileNameWithoutExtension(entry.Path), MessageState.WARNING);
                                    }

                                }
                                else
                                {
                                    //   logger.Write(entry.Path + " has no value for" + property.Name, MessageState.WARNING);
                                }
                            }
                        }
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

                records.AddElements();
                logger.Write("Synchronized: " + type.Name);

            }
            logger.Write("Done", MessageState.IMPORTANT_INFO);
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
