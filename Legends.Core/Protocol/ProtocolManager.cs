using Legends.Core.IO;
using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol
{

    public class ProtocolManager
    {
        public const string ID_MESSAGE_FIELD_NAME = "PACKET_CMD";

        public const string CHANNEL_MESSAGE_FIELD_NAME = "CHANNEL";

        private static readonly Type[] HandlerMethodParameterTypes = new Type[] { typeof(Message), typeof(ENetClient) };

        private static readonly Dictionary<PacketCmd, Delegate> Handlers = new Dictionary<PacketCmd, Delegate>();

        private static readonly Dictionary<PacketCmd, Dictionary<Channel, Type>> Messages = new Dictionary<PacketCmd, Dictionary<Channel, Type>>();

        private static readonly Dictionary<PacketCmd, Dictionary<Channel, Func<Message>>> Constructors = new Dictionary<PacketCmd, Dictionary<Channel, Func<Message>>>();

        public static int MessageCount
        {
            get
            {
                return Messages.Count;
            }
        }
        public static Logger logger = new Logger();

        public static bool ShowProtocolMessage;
        /// <param name="messagesAssembly"></param>
        /// <param name="handlersAssembly"></param>
        public static void Initialize(Assembly serverAssembly, bool showProtocolMessages)
        {
            ShowProtocolMessage = showProtocolMessages;
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(Message))))
            {
                FieldInfo numField = type.GetField(ID_MESSAGE_FIELD_NAME);
                FieldInfo channelField = type.GetField(CHANNEL_MESSAGE_FIELD_NAME);
                if (numField != null)
                {
                    PacketCmd num = (PacketCmd)numField.GetValue(type);
                    Channel channel = (Channel)channelField.GetValue(type);

                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);


                    if (constructor == null)
                        throw new Exception(string.Format("'{0}' doesn't implemented a parameterless constructor", type));


                    if (Messages.ContainsKey(num))
                        Messages[num].Add(channel, type);
                    else
                    {
                        Messages.Add(num, new Dictionary<Channel, Type>()
                    {
                        { channel, type }
                    });
                    }

                    Func<Message> func = constructor.CreateDelegate<Func<Message>>();

                    if (Constructors.ContainsKey(num))
                        Constructors[num].Add(channel, func);
                    else
                    {
                        var dic = new Dictionary<Channel, Func<Message>>() { { channel, func } };
                        Constructors.Add(num, dic);
                    }
                }
            }

            foreach (var item in serverAssembly.GetTypes())
            {
                foreach (var subItem in item.GetMethods())
                {
                    var attribute = subItem.GetCustomAttribute(typeof(MessageHandlerAttribute));
                    if (attribute != null)
                    {
                        ParameterInfo[] parameters = subItem.GetParameters();
                        Type methodParameters = subItem.GetParameters()[0].ParameterType;
                        if (methodParameters.BaseType != null)
                        {
                            try
                            {
                                Delegate target = subItem.CreateDelegate(HandlerMethodParameterTypes);
                                FieldInfo field = methodParameters.GetField(ID_MESSAGE_FIELD_NAME);
                                Handlers.Add((PacketCmd)field.GetValue(null), target);
                            }
                            catch
                            {
                                throw new Exception("Cannot register " + subItem.Name + " has message handler...");
                            }

                        }
                    }

                }
            }

            if (ShowProtocolMessage)
                logger.Write(MessageCount + " Message(s) Loaded | " + Handlers.Count + " Handler(s) Loaded");
        }
        /// <summary>
        /// Unpack message
        /// </summary>
        /// <param name="id">Id of the message</param>
        /// <param name="reader">Reader with the message datas</param>
        /// <returns></returns>
        private static Message ConstructMessage(PacketCmd id, Channel channel, LittleEndianReader reader)
        {
            if ((byte)id == 0x64) // some exeptions, architecture problems with LoL protocol.
            {
                if (channel == Channel.CHL_C2S)
                    id = PacketCmd.PKT_C2S_ClientReady;
                if (channel == Channel.CHL_S2C)
                    id = PacketCmd.PKT_S2C_Dash;
            }



            if (!Messages.ContainsKey(id))
            {
                return null;
            }
            Message message = Constructors.FirstOrDefault(x=>x.Key == id).Value[channel]();
            if (message == null)
            {
                return null;
            }
            message.Unpack(reader);
            return message;
        }

        public static Dictionary<PacketCmd, Delegate> GetHandlers(PacketCmd[] ids)
        {
            return Handlers.Where(x => ids.Contains(x.Key)).ToDictionary(x => x.Key, y => y.Value);
        }
        public static bool HandleMessage(Message message, ENetClient client)
        {
            if (message == null)
            {
                // client.Disconnect();
                return false;
            }

            var handler = Handlers.FirstOrDefault(x => x.Key == message.Cmd);

            if (handler.Value != null)
            {
                {
                    if (ShowProtocolMessage)
                    {
                        logger.Write("Receive " + message.ToString(), MessageState.INFO);
                    }

                    try
                    {

                        handler.Value.DynamicInvoke(null, message, client);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        logger.Write(string.Format("Unable to handle message {0} {1} : '{2}'", message.ToString(), handler.Value.Method.Name, ex.InnerException.ToString()), MessageState.WARNING);
                        return false;
                    }
                }
            }
            else
            {
                if (ShowProtocolMessage)
                    logger.Write(string.Format("No Handler: ({0}) {1}", message.Cmd.ToString(), message.ToString()), MessageState.IMPORTANT_INFO);
                return true;
            }
        }
        /// <summary>
        /// Build a messagePart and call the ConstructMessage(); method.
        /// </summary>
        /// <param name="buffer">data received</param>
        /// <returns>Message of your protocol, builted</returns>
        public static Message BuildMessage(ENetClient client, Channel channel, byte[] buffer)
        {
            var reader = new LittleEndianReader(buffer);

            PacketCmd messageId = (PacketCmd)reader.ReadByte();

            Message message;
            try
            {
                message = ConstructMessage(messageId, channel, reader);

                if (message == null)
                {
                    logger.Write("Message: " + messageId + " not registered in protocol", MessageState.WARNING);
                }
                return message;
            }
            catch (Exception ex)
            {
                logger.Write("Exception while building Message : (" + messageId + ") =>" + ex.Message, MessageState.WARNING);
                return null;
            }
            finally
            {
                reader.Dispose();
            }


        }
    }
}
