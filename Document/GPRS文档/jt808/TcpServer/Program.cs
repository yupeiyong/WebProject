using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 86;
            var server=new Server("192.168.1.116",port);
            server.Start();
            Console.ReadKey();
        }
    }

    class Server
    {
        private string ip;
        private int port;
        public Server(string ip,int port)
        {
            this.ip = ip;
            this.port = port;

        }

        public void Start()
        {
            Console.WriteLine("服务端已经启动！");
            var paramter = new {Ip=ip,Port=port};
            Task.Factory.StartNew(obj =>
            {
                var o = (dynamic)obj;
               Init(o);
            }, paramter);

        }

        private void Init(dynamic obj)
        {
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(new IPEndPoint(IPAddress.Parse(obj.Ip), obj.Port));
            s.Listen(50);

            var client = s.Accept();
            while (true)
            {
                var buffers = new byte[2048];
                var count=client.Receive(buffers);
                var str = Encoding.UTF8.GetString(buffers, 0, count);
                Console.WriteLine(str);

                //发送回消息到客户端
                var clientIp = client.RemoteEndPoint;
                var msg = string.Format("收到客户端{0}消息。",clientIp.ToString());
                client.Send(Encoding.UTF8.GetBytes(msg));
                Thread.Sleep(2000);

            }
        }
    }
}
