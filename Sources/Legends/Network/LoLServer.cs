using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static ENet.Native;
using System.Threading.Tasks;
using Legends.Core.Cryptography;
using ENet;
using System.Threading;
using Legends.Core.Utils;
using Legends.Core.Protocol;
using Legends.Configurations;
using Legends.Core.IO;
using Legends.Core.DesignPattern;

namespace Legends.Network
{
    public unsafe class LoLServer
    {
        private static uint SERVER_HOST = ENET_HOST_ANY;
        public const int SERVER_PORT = 5119;

        public const string SERVER_KEY = "17BLOhi6KZsTtldTsizvHg==";
        public const string CLIENT_REQUIRED_VERSION = "Version 4.20.0.315 [PUBLIC]";

        public const int PEER_MTU = 996;
        public const double REFRESH_RATE = 16.66;

        static Dictionary<uint, LoLClient> m_clients;

        static ENetHost* _server;


        public static BlowFish BlowFish;

        static Logger logger = new Logger();

        [StartupInvoke("Server", StartupInvokePriority.Last)]
        public static void Initialize()
        {

            m_clients = new Dictionary<uint, LoLClient>();
            if (enet_initialize() < 0)
                throw new Exception("Unable to initialize ENet...");

            var address = new ENetAddress();
            address.host = SERVER_HOST;
            address.port = ConfigurationProvider.Instance.Configuration.ServerPort;

            _server = enet_host_create(&address, new IntPtr(32), new IntPtr(32), 0, 0);

            if (_server == null)
                throw new Exception("Unable to start ENet server...");

            var key = Convert.FromBase64String(SERVER_KEY);

            if (key.Length <= 0)
                throw new Exception("Unable to convert SERVER_KEY...");

            BlowFish = new BlowFish(key);

        }

        private static bool Broadcast(byte[] buffer, Channel channelNo, PacketFlags packetFlags = PacketFlags.None)
        {
            fixed (byte* b = buffer)
            {
                if (buffer.Length >= 8)
                    BlowFish.Encrypt(buffer);

                var packet = enet_packet_create(new IntPtr(b), new IntPtr(buffer.Length), packetFlags);
                enet_host_broadcast(LoLServer.GetServer(), (byte)channelNo, packet);
            }
            return true;
        }
        public static void Broadcast(Message message, Channel channelNo, PacketFlags flags = PacketFlags.Reliable)
        {
            LittleEndianWriter writer = new LittleEndianWriter();
            message.Pack(writer);
            var packet = writer.Data;
            Broadcast(packet, channelNo, flags);

            if (ProtocolManager.ShowProtocolMessage)
                ProtocolManager.logger.Write("Send (Broadcast) " + message.ToString() + "(" + (int)message.Cmd + ")" + " (" + packet.Length + "b)", MessageState.INFO2);
        }
        public static void NetLoop()
        {
            var enetEvent = new ENetEvent();
            while (true)
            {
                while (enet_host_service(_server, &enetEvent, 0) > 0)
                {
                    switch (enetEvent.type)
                    {
                        case EventType.Connect:
                            //Logging->writeLine("A new client connected: %i.%i.%i.%i:%i", event.peer->address.host & 0xFF, (event.peer->address.host >> 8) & 0xFF, (event.peer->address.host >> 16) & 0xFF, (event.peer->address.host >> 24) & 0xFF, event.peer->address.port);

                            /* Set some defaults */
                            enetEvent.peer->mtu = PEER_MTU;
                            enetEvent.data = 0;
                            m_clients.Add(enetEvent.peer->connectID, CreateClient(enetEvent.peer));
                            logger.Write("Client connected", MessageState.IMPORTANT_INFO);
                            break;

                        case EventType.Receive:
                            m_clients[enetEvent.peer->connectID].OnDataArrival(enetEvent.packet, (Channel)enetEvent.channelID);
                            enet_packet_destroy(enetEvent.packet);
                            break;

                        case EventType.Disconnect:
                            if (enetEvent.peer->connectID == 0)
                            {
                                return;
                            }
                            m_clients[enetEvent.peer->connectID].OnDisconnect();
                            m_clients.Remove(enetEvent.peer->connectID);
                            logger.Write("Client disconnected", MessageState.IMPORTANT_INFO);
                            break;
                    }
                }
                Thread.Sleep((int)REFRESH_RATE);
            }
        }
        public static LoLClient[] GetClients()
        {
            return m_clients.Values.ToArray();
        }
        public static ENetHost* GetServer()
        {
            return _server;
        }

        private static LoLClient CreateClient(ENetPeer* source)
        {
            return new LoLClient(source);
        }

    }
}
