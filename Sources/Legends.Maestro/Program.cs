using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Maestro
{
    /// <summary>
    /// Credit to Maufeat and Xupwup's Thanks to us! 
    /// https://github.com/Maufeat/Maestro
    /// </summary>
    class Program
    {
        static Logger logger = new Logger();

        private const int PORT = 8393;

        private static MaestroServer Server;

        static void Main(string[] args)
        {
            logger.OnStartup();

            Server = new MaestroServer(PORT);
        }
    }


}
