using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartORM.IO
{
    public class SmartTable
    {
        /// <summary>
        /// Key = Filename
        /// Value = JsonContent
        /// </summary>
        public List<TablePair> Files;

        public TablePair GetTablePair(string fileName)
        {
            return Files.FirstOrDefault(x => x.FileName == fileName);
        }
    }
    public class TablePair
    {
        public string FileName;

        public string Value;

        public TablePair(string fileName,string value)
        {
            this.FileName = fileName;
            this.Value = value;
        }
    }
}
