using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Utils
{
    public enum MessageState
    {
        INFO = 0,
        INFO2 = 1,
        IMPORTANT_INFO = 2,
        WARNING = 3,
        ERROR = 4,
        ERROR_FATAL = 5,
        SUCCES = 6,
    }
    public class Logger
    {
        private const ConsoleColor COLOR_1 = ConsoleColor.Magenta;
        private const ConsoleColor COLOR_2 = ConsoleColor.DarkMagenta;

        private Dictionary<MessageState, ConsoleColor> Colors = new Dictionary<MessageState, ConsoleColor>()
        {
            { MessageState.INFO,            ConsoleColor.Gray },
            { MessageState.INFO2,           ConsoleColor.DarkGray },
            { MessageState.IMPORTANT_INFO,  ConsoleColor.White },
            { MessageState.SUCCES,          ConsoleColor.Green },
            { MessageState.WARNING,         ConsoleColor.Yellow },
            { MessageState.ERROR ,          ConsoleColor.DarkRed},
            { MessageState.ERROR_FATAL,     ConsoleColor.Red }
        };

        static string ClassName;

        static Logger()
        {
            StackFrame frame = new StackFrame(1, false);
            ClassName = frame.GetMethod().DeclaringType.Name;
        }
        private void Logo()
        {
            WriteColor2(" __                      _     ", false);
            WriteColor2("|  |   ___ ___ ___ ___ _| |___ ", false);
            WriteColor2("|  |__| -_| . | -_|   | . |_ -|", false);
            WriteColor2("|_____|___|_  |___|_|_|___|___|", false);
            WriteColor2("          |___|                ", false);
            WriteColor1(@"(League Of Legends 4.20)", false);
        }
        public void Write(object value, MessageState state = MessageState.INFO)
        {
            WriteColored(value, Colors[state], true);
        }
        public void WriteColor1(object value, bool type = true)
        {
            WriteColored(value, COLOR_1, type);
        }
        public void WriteColor2(object value, bool type = true)
        {
            WriteColored(value, COLOR_2, type);
        }
        private void WriteColored(object value, ConsoleColor color, bool type = true)
        {
            Console.ForegroundColor = color;
            if (type)
                Console.WriteLine("(" + ClassName + ") " + value);
            else
                Console.WriteLine(value);
        }
        public void NewLine()
        {
            Console.WriteLine(Environment.NewLine);
        }
        public void OnStartup()
        {
            Console.Title = Assembly.GetCallingAssembly().GetName().Name;
            Logo();
            NewLine();
        }
    }
}
