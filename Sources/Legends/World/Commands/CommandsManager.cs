using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.Core.Protocol.Other;
using Legends.Core.Utils;
using Legends.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Commands
{
    public class CommandsManager : Singleton<CommandsManager>
    {
        public const string COMMANDS_PREFIX = ".";

        private Dictionary<string, MethodInfo> Handlers = new Dictionary<string, MethodInfo>();

        [StartupInvoke("Commands", StartupInvokePriority.Eighth)]
        public void Initialize()
        {
            foreach (var method in typeof(CommandsRepertory).GetMethods())
            {
                CommandAttribute attribute = method.GetCustomAttribute<CommandAttribute>();

                if (attribute != null)
                {
                    Handlers.Add(attribute.name, method);
                }
            }
        }
        public void Handle(LoLClient client, string content)
        {
            string[] info = content.Split(null);
            string name = info[0].Substring(1, info[0].Length - 1);

            if (Handlers.ContainsKey(name))
            {
                MethodInfo method = Handlers[name];
                List<object> param = new List<object>() { client };

                string[] paramString = info.ToArray();
                ParameterInfo[] methodParams = method.GetParameters();

                try
                {
                    for (int i = 1; i < methodParams.Length; i++)
                    {
                        param.Add(Convert.ChangeType(paramString[i], methodParams[i].ParameterType));
                    }

                    method.Invoke(this, param.ToArray());
                }
                catch
                {
                    client.Hero.DebugMessage(name + " usage: [" + string.Join("] [", Array.ConvertAll(methodParams.Skip(1).ToArray(), x => x.ParameterType.Name)) + "]");
                }
            }
            else
            {
                client.Hero.DebugMessage("Available commands: ." + string.Join(" " + COMMANDS_PREFIX, Handlers.Keys));
            }
        }


    }
}
