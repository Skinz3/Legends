using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Maestro
{
    public class MaestroServer
    {
        static Logger logger = new Logger();

        private TcpListener Socket
        {
            get;
            set;
        }

        public MaestroServer(int port)
        {
            this.Socket = new TcpListener(IPAddress.Any, port);

            var t = Task.Run(() =>
            {
                logger.Write($"Maestro Server started.");
                while (true)
                {
                    this.Socket.Start();
                    var task = Socket.AcceptTcpClientAsync();
                    task.Wait();
                    new MaestroClient(task.Result, new byte[4096]);
                }
            });
            t.Wait();
        }
    }
}
