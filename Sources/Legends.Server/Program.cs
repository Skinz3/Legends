using Legends.Configurations;
using Legends.Core.CSharp;
using Legends.Core.DesignPattern;
using Legends.Core.Protocol;
using Legends.Core.Time;
using Legends.Core.Utils;
using Legends.Network;
using Legends.ORM;
using Legends.Protocol.GameClient.LoadingScreen;
using Legends.Records;
using Legends.World.Champions;
using Legends.World.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Legends
{
    class Program
    {
        static Logger logger = new Logger();

        public const string DATABASE_FILENAME = "database.smart";

        static void Main(string[] args)
        {
            logger.OnStartup();
            StartupManager.Instance.Initialize(Assembly.GetAssembly(typeof(AIUnitRecord)));
            logger.Write("Server started");


            if (Debugger.IsAttached)
            {
                for (int i = 0; i < ConfigurationProvider.Instance.Configuration.Players.Count; i++)
                {
                    Process.Start("StartGame"+(i+1)+".bat");
                }
            }

            LoLServer.NetLoop();

            Console.ReadKey();
        }

       

        [StartupInvoke("Database", StartupInvokePriority.First)]
        public static void LoadDatabase()
        {
            DatabaseManager.Instance.Initialize(Assembly.GetAssembly(typeof(AIUnitRecord)), "127.0.0.1", "legends", "root", "");
            DatabaseManager.Instance.LoadTables();
        }
        [StartupInvoke("CSharp Scripts", StartupInvokePriority.Third)]
        public static void LoadScripts()
        {
            InjectionManager.Instance.Initialize(new Assembly[] { Assembly.GetAssembly(typeof(AIUnitRecord)), Assembly.GetAssembly(typeof(Script)), Assembly.GetAssembly(typeof(KeyCheckMessage)) });
        }
        [StartupInvoke("Protocol", StartupInvokePriority.Second)]
        public static void LoadProtocol()
        {
            ProtocolManager.Initialize(Assembly.GetAssembly(typeof(KeyCheckMessage)), Assembly.GetExecutingAssembly(), true);
        }
    }
}