using System;

namespace Legends.ORM.Attributes
{
    public class TableAttribute : Attribute
    {
        public string tableName;
        public bool catchAll;
        public short readingOrder;

        public TableAttribute(string tableName, short readingOrder = -1, bool catchAll = true)
        {
            this.tableName = tableName;
            this.catchAll = catchAll;
            this.readingOrder = readingOrder;
        }
    }
}
