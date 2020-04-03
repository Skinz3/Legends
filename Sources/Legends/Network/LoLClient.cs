﻿using ENet;
using Legends.Core.Cryptography;
using Legends.Core.DesignPattern;
using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Core.Utils;
using Legends.World;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Games;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ENet.Native;

namespace Legends.Network
{
    public unsafe class LoLClient : ENetClient
    {
        Logger logger = new Logger();

        public bool Ready
        {
            get;
            set;
        }
        public bool Encrypt
        {
            get;
            set;
        }
        public long? UserId
        {
            get;
            set;
        }

        public override string Ip => "An Ip ";

        public AIHero Hero
        {
            get;
            private set;
        }
        public LoLClient(ENetPeer* peer) : base(peer)
        {
            Ready = false;
        }
        public void DefinePlayer(AIHero hero)
        {
            this.Hero = hero;
        }
        public void OnDataArrival(ENetPacket* packet, Channel channel)
        {


            var data = new byte[(int)packet->dataLength];
            Marshal.Copy(packet->data, data, 0, data.Length);

            if ((int)packet->dataLength >= 8 && Encrypt)
            {
                data = LoLServer.BlowFish.Decrypt(data);
            }
            LittleEndianReader reader = new LittleEndianReader(data);

            Message message = ProtocolManager.BuildMessage(this, channel, data);
            ProtocolManager.HandleMessage(message, this);
        }
        [InDevelopment(InDevelopmentState.THINK_ABOUT_IT, "A good synchronization method?")]
        public override void OnMessageHandle(Message message, Delegate handler)
        {
            Handle(message, handler);
        }
        private void Handle(Message message, Delegate handler)
        {
            try
            {
                handler.DynamicInvoke(null, message, this);
            }

            catch (Exception ex)
            {
                logger.Write("Exception on message (" + message.ToString() + "): " + ex.ToString(), MessageState.ERROR);
            }
        }
        public bool Send(byte[] buffer, Channel channelNo, PacketFlags flag = PacketFlags.Reliable)
        {
            if (buffer.Length >= 8)
                buffer = LoLServer.BlowFish.Encrypt(buffer);
            fixed (byte* data = buffer)
            {
                var packet = enet_packet_create(new IntPtr(data), new IntPtr(buffer.Length), flag);
                if (enet_peer_send(Peer, (byte)channelNo, packet) < 0)
                    return false;
            }
            return true;
        }
        public void Send(Message message, Channel channelNo = Channel.CHL_S2C, PacketFlags flags = PacketFlags.Reliable)
        {
            LittleEndianWriter writer = new LittleEndianWriter();
            message.Pack(writer);
            var packet = writer.Data;
            this.Send(packet, channelNo, flags);

            if (ProtocolManager.ShowProtocolMessage)
                ProtocolManager.logger.Write("Send " + message.ToString() + "(" + (int)message.Cmd + ")" + " (" + packet.Length + "b)", MessageState.INFO2);
        }
        public override void Disconnect()
        {
            Console.WriteLine("todo disconnect");
        }

        public void OnDisconnect()
        {
            Hero?.OnDisconnect();
        }
    }
}
