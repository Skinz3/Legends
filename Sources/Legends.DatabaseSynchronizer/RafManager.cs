using Legends.Core.IO.Inibin;
using Legends.Core.IO.RAF;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer
{
    public class RafManager : IDisposable
    {
        public const string ARCHIVE_RELATIVE_PATH = @"RADS\projects\lol_game_client\filearchives\";
        public const string ARCHIVE_EXTENSION = ".raf";

        public string ClientPath
        {
            get;
            private set;
        }
        public string ArchivesPath
        {
            get
            {
                return ClientPath + ARCHIVE_RELATIVE_PATH;
            }
        }
        public RAF[] Archives
        {
            get;
            private set;
        }
        public RafManager(string clientPath)
        {
            this.ClientPath = clientPath;
            this.Load();
        }
        private void Load()
        {
            List<RAF> rafs = new List<RAF>();

            foreach (var directory in Directory.GetDirectories(ArchivesPath))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (Path.GetExtension(file) == ARCHIVE_EXTENSION)
                    {
                        rafs.Add(new RAF(file));
                    }
                }
            }
            this.Archives = rafs.ToArray();
        }
        public string[] GetDirectories(string path)
        {
            List<string> results = new List<string>();

            foreach (var archive in Archives)
            {
                var files = archive.Files.FindAll(x => x.Path.Contains(path));

                foreach (var file in files)
                {
                    var splitted = file.Path.Substring(path.Length, file.Path.Length - path.Length).Split('/');

                    if (splitted.Length > 1)
                    {
                        results.Add(splitted[1]);
                    }
                }
            }

            return results.Distinct().ToArray();
        }
        private RAFFileEntry[] GetFile(string path)
        {
            List<RAFFileEntry> results = new List<RAFFileEntry>();

            foreach (var archive in Archives)
            {
                var result = archive.Files.FirstOrDefault(x => x.Path == path);

                if (result != null)
                {
                    results.Add(result);
                }
            }
            return results.ToArray();
        }
        /// <summary>
        /// .LastOrDefault() return the file up to date. (if there is two files, the first ones are previous version.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RAFFileEntry GetUpToDateFile(string path) // "DATA/Characters/Aatrox/Aatrox.inibin"
        {
            return GetFile(path).LastOrDefault();
        }
        /// <summary>
        /// Return up to date files
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public RAFFileEntry[] GetFilesInDirectory(string dirPath, string extension)
        {
            Dictionary<string, RAFFileEntry> results = new Dictionary<string, RAFFileEntry>();

            var files = GetFiles(dirPath);

            foreach (var file in files)
            {
                var split = file.Path.Split('/');
                string path = string.Join("/", split.Take(split.Length - 1).ToArray());

                if (path == dirPath && Path.GetExtension(file.Path) == extension)
                {
                    if (results.ContainsKey(file.Path))
                    {
                        results[file.Path] = file;
                    }
                    else
                    {
                        results.Add(file.Path, file);
                    }
                }
            }

            return results.Values.ToArray();
        }
        public RAFFileEntry[] GetFiles(string containsPath)
        {
            List<RAFFileEntry> results = new List<RAFFileEntry>();

            foreach (var archive in Archives)
            {
                foreach (var file in archive.Files)
                {
                    if (file.Path.Contains(containsPath))
                    {
                        results.Add(file);
                    }
                }
            }
            return results.ToArray();
        }

        public void Dispose()
        {
            foreach (var raf in Archives)
            {
                raf.Dispose();
            }
            Archives = new RAF[0];
        }
    }
}
