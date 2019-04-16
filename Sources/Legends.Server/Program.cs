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
using Legends.World.Commands;
using System;
using Legends.Core;
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
using Legends.World.Games;
using System.Globalization;

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


            if (ConfigurationProvider.Instance.Configuration.StartClient)
            {
                foreach (var player in ConfigurationProvider.Instance.Configuration.Players)
                {
                    logger.WriteColor1("Starting League of Legends for userId: " + player.UserId);

                    ClientHooks.StartClient(ConfigurationProvider.Instance.Configuration.LeaguePath,
                 ConfigurationProvider.Instance.Configuration.ServerIp,
                 ConfigurationProvider.Instance.Configuration.ServerPort,
                 LoLServer.SERVER_KEY, player.UserId);
                }

            }

            GameProvider.GameLoop();

            while (true)
                Console.ReadKey();
        }



        [StartupInvoke("Database", StartupInvokePriority.First)]
        public static void LoadDatabase()
        {
            DatabaseManager.Instance.Initialize(Assembly.GetAssembly(typeof(AIUnitRecord)),
               ConfigurationProvider.Instance.Configuration.MySQLHost, ConfigurationProvider.Instance.Configuration.DatabaseName
               , ConfigurationProvider.Instance.Configuration.MySQLUser, ConfigurationProvider.Instance.Configuration.MySQLPassword);

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
            ProtocolManager.Initialize(Assembly.GetAssembly(typeof(KeyCheckMessage)), Assembly.GetExecutingAssembly(), false);
        }
    }
}