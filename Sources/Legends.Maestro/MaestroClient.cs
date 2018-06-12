using Legends.Core.IO;
using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Legends.Maestro
{
    public class MaestroClient
    {
        static Logger logger = new Logger();

        private TcpClient Socket
        {
            get;
            set;
        }
        private NetworkStream Stream
        {
            get;
            set;
        }
        private System.Timers.Timer hbTimer;
        private byte[] Buffer
        {
            get;
            set;
        }

        public MaestroClient(TcpClient socket, byte[] buffer)
        {
            this.Socket = socket;
            this.Stream = Socket.GetStream();
            this.Buffer = buffer;

            logger.Write("New client connected, starting Hearbeat.");

            this.hbTimer = new Timer(25000);
            this.hbTimer.Elapsed += async (sender, e) => await HandleHeartbeat(this);
            this.hbTimer.Start();

            receiveData(null);
        }


        private static Task HandleHeartbeat(MaestroClient client)
        {
            LittleEndianWriter writer = new LittleEndianWriter();

            writer.WriteInt((int)16);
            writer.WriteInt((int)1);
            writer.WriteInt((int)MessageTypeEnum.HEARTBEAT);
            writer.WriteInt((int)0);

            var heatBeatData = writer.Data;

            writer.Dispose();

            logger.Write("Heartbeat sended.");
            client.Stream.Write(heatBeatData, 0, heatBeatData.Length);
            return Task.CompletedTask;
        }

        private object _lock = new object();

        private async void receiveData(Task<int> result)
        {
            if (result != null)
            {
                lock (_lock)
                {
                    int numberOfBytesRead = result.Result;
                    if (numberOfBytesRead == 0)
                    {
                        onDisconnected();
                        return;
                    }
                    var segmentedArr = new ArraySegment<byte>(Buffer, 0, numberOfBytesRead).ToArray();
                    OnDataReceived(segmentedArr);
                }

            }
            var task = Stream.ReadAsync(Buffer, 0, Buffer.Length);
            await task.ContinueWith(receiveData);
        }

        private void onDisconnected()
        {
            hbTimer.Stop();
        }

        private void OnDataReceived(byte[] dat)
        {
            Stream stream = new MemoryStream(dat);

            int HeaderLength, Version, Type, DataLength;

            using (BinaryReader reader = new BinaryReader(stream))
            {
                HeaderLength = reader.ReadInt32();
                Version = reader.ReadInt32();
                Type = reader.ReadInt32();
                DataLength = reader.ReadInt32();

                // TODO: Handle more MessageTypes
                switch ((MessageTypeEnum)Type)
                {
                    case MessageTypeEnum.HEARTBEAT:
                        logger.Write("Received Heartbeat");
                        break;
                    case MessageTypeEnum.EXIT:
                        logger.Write("Client closed.");
                        break;
                    default:
                        logger.Write("Received Unknown Type :" + Type);
                        break;
                }
            }
        }
    }
}
