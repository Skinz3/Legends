using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer
{
    public class JSONHashes
    {
        public Dictionary<uint, string> Values = new Dictionary<uint, string>();


        public JSONHashes(string path, string prefix)
        {
            var a = File.ReadAllLines(path);

            Dictionary<string, uint> hashes = new Dictionary<string, uint>();

            for (int i = 2; i < a.Length - 2; i += 5)
            {
                var index = a[i].Split('"')[1];
                string val = a[i + 2].Split('"')[1];
                Values.Add(uint.Parse(index), val);
            }
            List<string> lines = new List<string>();

            foreach (var val in Values)
            {

                if (lines.FirstOrDefault(x => x.Contains(val.Value)) == null)
                {
                    lines.Add(prefix + "_" + val.Value + "=" + val.Key + ",");
                }
            }

            File.Delete(Environment.CurrentDirectory + "/test.txt");
            File.AppendAllLines(Environment.CurrentDirectory + "/test.txt", lines);
            Process.Start(Environment.CurrentDirectory + "/test.txt");
            Thread.Sleep(2000);
            Environment.Exit(0);

        }
    }
}
