using System;

namespace Legends.ORM.Attributes
{
    public class TableAttribute : Attribute
    {
        public string path;

        public TableAttribute(string path)
        {
            this.path = path;
        }
    }
}
