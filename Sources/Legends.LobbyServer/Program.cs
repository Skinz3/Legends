using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Legends.LobbyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener list = new HttpListener();
            list.Prefixes.Add("http://127.0.0.1:8080/");
            list.Start();

            while (list.IsListening)
            {
                HttpListenerContext context = list.GetContext();
                var infos = context.Request;
                byte[] buffer = new byte[infos.ContentLength64];
                infos.InputStream.Read(buffer, 0, buffer.Length);
                StreamReader reader = new StreamReader(new MemoryStream(buffer), infos.ContentEncoding);
                string data = reader.ReadToEnd();
                 
                string reponse = "{\"rate\":0,\"reason\":\"invalid_credentials\",\"status\":\"FAILED\",\"delay\":5000}";
                    
                var buff = Encoding.ASCII.GetBytes(reponse);
                context.Response.ContentLength64 = buff.Length;
                context.Response.StatusCode = 403;
                context.Response.OutputStream.Write(buff, 0, buff.Length);
            }
        }
    }
}
