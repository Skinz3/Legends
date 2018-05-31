using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.DesignPattern
{
    public class StartupInvoke : Attribute
    {
        public StartupInvokePriority Type
        {
            get; set;
        }

        public bool Hided
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public StartupInvoke(string name, StartupInvokePriority type)
        {
            this.Type = type;
            this.Name = name;
            this.Hided = false;
        }
        public StartupInvoke(StartupInvokePriority type)
        {
            this.Hided = true;
            this.Type = type;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
