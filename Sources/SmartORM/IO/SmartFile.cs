using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartORM.IO
{
    public class SmartFile
    {
        public const string HEADER = "_SM";

        /// <summary>
        /// Key = Path
        /// Values = Files
        /// </summary>
        private Dictionary<string, SmartTable> Tables
        {
            get;
            set;
        }
        public string Path
        {
            get;
            private set;
        }
        public SmartFile(string path)
        {
            this.Path = path;
            this.Read();
        }
        public SmartFile(string path, Dictionary<string, SmartTable> tables)
        {
            this.Path = path;
            this.Tables = tables;
        }
        public void AddTable(ITable table)
        {
            string path = ITableManager.Instance.GetFilePath(table);
            string json = JsonConvert.SerializeObject(table);
            string dirName = ITableManager.Instance.GetTableAttribute(table).Path;
            SmartTable target = FindSmartTable(dirName);
            target.Files.Add(new TablePair(path, json));
        }
        public void UpdateTable(ITable table)
        {
            string path = ITableManager.Instance.GetFilePath(table);
            FindSmartTable(ITableManager.Instance.GetTableAttribute(table).Path).GetTablePair(path).Value = JsonConvert.SerializeObject(table);
        }
        public void RemoveTable(ITable table)
        {
            string path = ITableManager.Instance.GetFilePath(table);
            SmartTable target = FindSmartTable(ITableManager.Instance.GetTableAttribute(table).Path);
            target.Files.Remove(target.GetTablePair(path));
        }
        private SmartTable FindSmartTable(string path)
        {
            if (Tables.ContainsKey(path))
            {
                return Tables[path];
            }
            else
            {
                SmartTable newTable = new SmartTable();
                newTable.Files = new List<TablePair>();
                Tables.Add(path, newTable);
                return newTable;
            }
        }
        public Dictionary<string,SmartTable> GetSmartTables()
        {
            return Tables;
        }
        public ITable[] ConvertToRecord(string path, Type type)
        {
            List<ITable> records = new List<ITable>();

            if (Tables.ContainsKey(path) == false)
            {
                throw new Exception("Table is missing: " + path);
            }
            SmartTable table = Tables[path];

            foreach (var file in table.Files)
            {
                records.Add((ITable)JsonConvert.DeserializeObject(file.Value, type));
            }

            return records.ToArray();
        }
        public void Save()
        {
            File.Delete(Path);
            MemoryStream targetStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(targetStream);

            writer.Write(HEADER);                 
                                                   
            writer.Write(Tables.Count);

            foreach (var table in Tables)
            {
                writer.Write(table.Key);
                writer.Write(table.Value.Files.Count);

                foreach (var file in table.Value.Files)
                {
                    writer.Write(file.FileName);
                    writer.Write(file.Value);
                }
            }
            File.WriteAllBytes(Path, targetStream.ToArray());
            writer.Dispose();
        }


        private void Read()
        {
            BinaryReader reader = new BinaryReader(new FileStream(Path, FileMode.Open));

            string header = reader.ReadString();

            if (header != HEADER)
            {
                throw new Exception(System.IO.Path.GetFileName(Path) + " is not a valid SmartFile.");
            }

            Tables = new Dictionary<string, SmartTable>();
            int count = reader.ReadInt32();


            for (int i = 0; i < count; i++)
            {
                SmartTable sTable = new SmartTable();

                string path = reader.ReadString();
                sTable.Files = new List<TablePair>();
                int tableFilesCount = reader.ReadInt32();


                for (int w = 0; w < tableFilesCount; w++)
                {
                    sTable.Files.Add(new TablePair(reader.ReadString(), reader.ReadString()));
                }

                Tables.Add(path, sTable);

            }

            reader.Dispose();
        }

    }
}
