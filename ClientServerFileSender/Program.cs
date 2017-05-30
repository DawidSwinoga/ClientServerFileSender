using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerFileSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(IPAddress.Parse("127.0.0.1"), 11000);
            server.Start();
        }
    }
}
