using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartORM.Attributes
{
    public class TableAttribute : Attribute
    {
        public string Path;

        public TableAttribute(string path)
        {
            this.Path = path;
        }
    }
}
