using Legends.Configurations;
using Legends.Core.DesignPattern;
using Legends.Core.Protocol;
using Legends.Core.Utils;
using Legends.Network;
using Legends.ORM;
using Legends.Records;
using Legends.World.Champions;
using Legends.World.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Legends
{
    class Program
    {
        static Logger logger = new Logger();

        static void Main(string[] args)
        {
            logger.OnStartup();
            StartupManager.Instance.Initialize(Assembly.GetAssembly(typeof(ChampionRecord)));
            logger.Write("Server started");
            Process.Start("StartGame.bat");
            Process.Start("StartGame2.bat");
            // Process.Start("StartGame3.bat");
            LoLServer.NetLoop();

            Console.ReadKey();
        }
        [StartupInvoke("Json Database", StartupInvokePriority.First)]
        public static void LoadDatabase()
        {
            DatabaseManager.Instance.Initialize(Environment.CurrentDirectory, Assembly.GetAssembly(typeof(ChampionRecord)));
            DatabaseManager.Instance.LoadTables();
        }
        [StartupInvoke("Protocol",StartupInvokePriority.Second)]
        public static void LoadProtocol()
        {
            ProtocolManager.Initialize(Assembly.GetExecutingAssembly(), true);
        }
    }
}
