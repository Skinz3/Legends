using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.IO.CFG
{
    public class CFGFile
    {
        public Dictionary<string, Dictionary<string, string>> Objects
        {
            get;
            private set;
        }
        public CFGFile(byte[] file)
        {
            Objects = new Dictionary<string, Dictionary<string, string>>();

            string text = Encoding.ASCII.GetString(file);
            string[] objects = text.Split(new string[] { "[" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var obj in objects)
            {
                string objName = obj.Split(']')[0];

                Objects.Add(objName, new Dictionary<string, string>());

                string[] properties = obj.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var property in properties.Skip(1))
                {
                    string[] val = property.Split('=');

                    if (val.Length == 2)
                    {
                        Objects[objName].Add(val[0], val[1]);
                    }
                }
            }

        }
    }
    public struct CFGProperty
    {
        public string Name;

        public string Value;

        public CFGProperty(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        public override string ToString()
        {
            return Name + "=" + Value;
        }
    }
}
