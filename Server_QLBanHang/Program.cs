using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_QLBanHang
{
    class Program
    {
        
            TcpListener server;
        public static List<Chat> clients = new List<Chat>();
        public static List<string> online = new List<string>();
        public Program()
        {
            server = new TcpListener(IPAddress.Any, 12345);
            server.Start();
            Console.WriteLine(">> This Is Server <<");
            while (true)
            {
                TcpClient soc = server.AcceptTcpClient();
                Chat newclient = new Chat(soc);
                Console.WriteLine("\n #" + newclient.id + " connected");
                newclient.sendata("<id>" + newclient.id + "</id>");
                newclient.starts();
                clients.Add(newclient);
            }
        }
        static void Main(string[] args)
        {
            new Program();

        }
    }
}

        }
    }
}
