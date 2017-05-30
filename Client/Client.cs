using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ClientServerFileSender;

namespace Client
{
    class Client
    {
        private IPAddress ipAddress;
        private int port;
        private TcpClient tcpClient;

        public Client(IPAddress ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public void SendFile(FileWrapper fileWrapper)
        {
            ConfigureSocket();
            SendFileToServer(fileWrapper);
        }

        private void SendFileToServer(FileWrapper fileWrapper)
        {
            IFormatter formatter = new BinaryFormatter();

            NetworkStream networkStream = tcpClient.GetStream();
            formatter.Serialize(networkStream, fileWrapper);
            tcpClient.Close();
        }

        private void ConfigureSocket()
        {
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
            tcpClient = new TcpClient();
            tcpClient.Connect(endPoint);
        }
    }
}
