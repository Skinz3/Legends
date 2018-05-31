using Legends.Configurations;
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
            ConfigurationManager.Instance.LoadConfiguration();
            LoadProtocol();
            LoadDatabase();
            CommandsManager.Instance.Initialize();
            ChampionManager.Instance.Initialize();
            LoLServer.Initialize();
            logger.Write("Server started");
            Process.Start("StartGame.bat");
            Process.Start("StartGame2.bat");
            // Process.Start("StartGame3.bat");
            LoLServer.NetLoop();

            Console.ReadKey();
        }
        private static void LoadDatabase()
        {
            DatabaseManager databaseManager = new DatabaseManager(Assembly.GetExecutingAssembly(),
                ConfigurationManager.Instance.Configuration.MySQLHost, ConfigurationManager.Instance.Configuration.DatabaseName,
                ConfigurationManager.Instance.Configuration.MySQLUser, ConfigurationManager.Instance.Configuration.MySQLPassword);

            databaseManager.LoadTables();
        }
        private static void LoadProtocol()
        {
            ProtocolManager.Initialize(Assembly.GetExecutingAssembly(), false);
        }
    }
}
