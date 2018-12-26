using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core
{
    public class ClientHooks
    {
        static Logger logger = new Logger();

        public static void StartClient(string leaguePath, string ip, int port, string serverKey, long userId)
        {
            if (Directory.Exists(leaguePath))
            {
                leaguePath = Path.Combine(leaguePath, "League of Legends.exe");
            }
            else
            {
                logger.Write("Unable to find League of Legends from the directory specified.", MessageState.ERROR);
            }

            if (File.Exists(leaguePath))
            {
                var startInfo = new ProcessStartInfo(leaguePath)
                {
                    Arguments = String.Format("\"8394\" \"LoLLauncher.exe\" \"\" \"{0} {1} {2} {3}\"",
                       ip, port, serverKey, userId),
                    WorkingDirectory = Path.GetDirectoryName(leaguePath)
                };

                var leagueProcess = Process.Start(startInfo);
            }
        }


    }
}
