using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartORM.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartORM.TableViewer
{
    public class TableView
    {
        private SmartFile SmartFile
        {
            get;
            set;
        }
        public string SelectedTable
        {
            get;
            set;
        }
        public TableView(SmartFile smartFile)
        {
            this.SmartFile = smartFile;
        }
        public string[] GetDirectoriesNames()
        {
            return SmartFile.GetSmartTables().Keys.ToArray();
        }
        public string[] GetProperties()
        {
            var files = SmartFile.GetSmartTables()[SelectedTable].Files;

            if (files.Count == 0)
            {
                return new string[0];
            }

            var objRef = files[0];

            var jObject = (JObject)JsonConvert.DeserializeObject(objRef.Value);

            return Array.ConvertAll(jObject.Properties().ToArray(), x => x.Name);
        }

        public List<KeyValuePair<string, object[]>> GetPropertiesValues()
        {
            List<KeyValuePair<string, object[]>> results = new List<KeyValuePair<string, object[]>>();

            foreach (var file in SmartFile.GetSmartTables()[SelectedTable].Files)
            {
                var jObject = (JObject)JsonConvert.DeserializeObject(file.Value);

                object[] properties = Array.ConvertAll<JToken, object>(jObject.PropertyValues().ToArray(), x => x.ToString());
                results.Add(new KeyValuePair<string, object[]>(file.FileName, properties));
            }

            return results;
        }

        public void Save()
        {
            SmartFile.Save();
        }

    }
}
