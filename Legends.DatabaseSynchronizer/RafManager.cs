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
    public class RafManager
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
        public RAFFileEntry GetFile(string path) // "DATA/Characters/Aatrox/Aatrox.inibin"
        {
            foreach (var archive in Archives)
            {
                var file = archive.Files.FirstOrDefault(x => x.Path == path);

                if (file != null)
                {
                    return file;
                }
            }
            return null;
        }
        public InibinFile GetInibinFile(string path)
        {
            byte[] file = GetFile(path).GetContent(true);
            return new InibinFile(new MemoryStream(file));
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


        

    }
}
