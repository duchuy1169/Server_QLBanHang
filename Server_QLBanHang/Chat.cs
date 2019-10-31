using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Server_QLBanHang
{
    class Chat
    {
        private StreamReader r;
        private StreamWriter w;
        private TcpClient soc;
        public static int count = 0;
        public string id;

        private void send(string data)
        {
            w.WriteLine(data);
            w.Flush();
        }

        private string rec()
        {
            return r.ReadLine();
        }

        public void starts()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(recdata));
        }

        public Chat(TcpClient socket)
        {
            ++count;
            id = "Client " + count.ToString().PadLeft(3, '0');
            soc = socket;
            r = new StreamReader(soc.GetStream());
            w = new StreamWriter(soc.GetStream());
        }

        public void sendata(string data)
        {
            try
            {
                send(data);
            }
            catch { }
        }

        public void recdata(object state)
        {
            string data = "";
            while (true)
            {
                try
                {
                    data = rec();
                    char[] token = new char[1];
                    token[0] = ':';
                    string[] s = data.Split(token);
                    string ma = s[0].ToString();

                    if (ma == "up")
                    {
                        update(data);
                    }

                    if (ma == "fi")
                    {
                        find(data);
                    }

                    if (ma == "ch")
                    {
                        update2(data);
                    }

                    if (ma == "cl")
                    {
                        remo(data);
                    }
                }
                catch { break; }
                if (data == null) break;
            }
            Console.WriteLine("\n ---" + id + " Disconnected");
            r.Close();
            w.Close();
            soc.Close();
        }


        private void update(string data)
        {
            Console.WriteLine();
            int dem = 1;
            int j = 0;
            char[] token = new char[1];
            token[0] = ':';
            string[] s = data.Split(token);
            string ma = s[1].ToString();
            foreach (string a in Program.online)
            {
                char[] token2 = new char[1];
                token2[0] = ':';
                string[] s2 = a.Split(token2);
                string ma2 = s2[1].ToString();
                if (ma == ma2)
                {
                    send("Existed");
                    j = 1;
                }
            }
            if (j != 1)
            {
                Program.online.Add(data);
                //Console.Clear();
                Console.WriteLine("Thông tin sản phẩm hiện có \n");

                foreach (string sim in Program.online)
                {
                    Console.WriteLine(dem + " : " + sim);
                    dem++;
                }
                Console.WriteLine();
            }
        }

        private void remo(string data)
        {
            string here = "";
            char[] token = new char[1];
            token[0] = ':';
            string[] s = data.Split(token);
            string ma = s[1].ToString();
            foreach (string a in Program.online)
            {
                char[] token2 = new char[1];
                token2[0] = ':';
                string[] s2 = a.Split(token2);
                string ma2 = s2[1].ToString();
                if (ma == ma2)
                {
                    here = a;
                    break;
                }
            }
            Program.online.RemoveAll(r => r == here);
        }


        private void find(string data)
        {
            int i = 0;
            char[] token = new char[1];
            token[0] = ':';
            string[] s = data.Split(token);
            string ma = s[1].ToString();
            foreach (string a in Program.online)
            {
                char[] token2 = new char[1];
                token2[0] = ':';
                string[] s2 = a.Split(token2);
                string ma2 = s2[1].ToString();
                if (ma == ma2)
                {
                    data = a.ToString();
                    send(data);
                    i = 1;
                }
            }
            if (i != 1)
            {
                send("Not Found");
            }
        }
        private void update2(string data)
        {
            int dem = 1;
            char[] token = new char[1];
            token[0] = ':';
            string[] s = data.Split(token);
            string ma = "up:" + s[1].ToString() + ":" + s[2].ToString() + ":" + s[3].ToString() + ":" + s[4].ToString() + ":" + s[5].ToString() + ":" + s[6].ToString() + ":" + s[7].ToString();
            Program.online.Add(ma);
            //Console.Clear();
            Console.WriteLine("\n");

            Console.WriteLine("Thông tin sản phẩm hiện có sau khi chỉnh sửa \n");
            foreach (string i in Program.online)
            {
                Console.WriteLine(dem + " : " + i);
                dem++;
            }
            Console.WriteLine();
        }
    }
}
