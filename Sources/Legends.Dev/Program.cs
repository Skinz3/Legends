using Legends.Core.DesignPattern;
using Legends.Core.Utils;
using Legends.Records;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Legends.Dev
{
    class Program
    {
        public static void Test()
        {

        }
        static Logger logger = new Logger();

        static void Main(string[] args)
        {
            Console.Read();
            logger.OnStartup();
            string result = DevelopmentManager.Analyse(Assembly.GetAssembly(typeof(AIUnitRecord)));
            logger.Write(result, MessageState.INFO);

        }
    }
}