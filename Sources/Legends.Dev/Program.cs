using Legends.Core.DesignPattern;
using Legends.Core.Utils;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Dev
{
    class Program
    {
        static Logger logger = new Logger();
        static void Main(string[] args)
        {
            logger.OnStartup();
            string result = DevelopmentManager.Analyse(Assembly.GetAssembly(typeof(AIUnitRecord)));
            logger.Write(result, MessageState.INFO);
            Console.Read();

        }
    }
}
