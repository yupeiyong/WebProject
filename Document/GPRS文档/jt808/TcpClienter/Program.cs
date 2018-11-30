using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpClienter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("输入连接地址：");
            var ip = Console.ReadLine();
            //"218.89.49.246",
            //125.66.97.11
            Console.WriteLine("输入连接端口：");
            var input = Console.ReadLine();
            var port = 0;
            if (int.TryParse(input, out port))
            {
                var clienter = new Clienter(ip, port);
                clienter.Start();
            }
            
            Console.ReadKey();
        }
    }

    class Clienter
    {
        private string ip;
        private int port;
        public Clienter(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

        }

        public void Start()
        {
            Console.WriteLine("客户端已经启动！");
            var paramter = new { Ip = ip, Port = port };
            Task.Factory.StartNew(obj =>
            {
                var o = (dynamic)obj;
                Init(o);
            }, paramter);

        }

        private void Init(dynamic obj)
        {
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(IPAddress.Parse(obj.Ip), obj.Port);

            var input = "";
            while (input!="exit")
            {
                input = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(input))continue;

                //发送回消息到服务器
                client.Send(Encoding.UTF8.GetBytes(input));

                var buffers = new byte[2048];
                var count = client.Receive(buffers);
                var str = Encoding.UTF8.GetString(buffers, 0, count);
                Console.WriteLine(str);
            }
        }
    }
}
