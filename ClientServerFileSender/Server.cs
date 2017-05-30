using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientServerFileSender
{
    class Server
    {
        private static readonly int DEFAULT_MAX_LENGTH_OF_CONNECTION_QUEUE = 100;
        private IPAddress ipAddress;
        private int port;
        private TcpListener tcpListener;
        private int maxLengthOfConnectionQueue;

        public Server(IPAddress ipAddress, int port, int maxLengthOfConnectionQueue)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.maxLengthOfConnectionQueue = maxLengthOfConnectionQueue;
        }

        public Server(IPAddress ipAddress, int port) : this(ipAddress, port, DEFAULT_MAX_LENGTH_OF_CONNECTION_QUEUE)
        {
            
        }

        public void Start()
        {
            ConfigureSocket();
            StartListener();
        }

        private void StartListener()
        {
            tcpListener.Start(maxLengthOfConnectionQueue);
            while (true)
            {
                Console.WriteLine("Waiting for a connection.");
                TcpClient client = tcpListener.AcceptTcpClient();

                Console.WriteLine("Connection accepted.");
                startClientThread(client);
            }
        }

        private void ConfigureSocket()
        {
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
            this.tcpListener = new TcpListener(endPoint);
        }

        private void startClientThread(TcpClient client)
        {
            new Thread(() =>
            {
                IFormatter formatter = new BinaryFormatter();
                NetworkStream networkStream = client.GetStream();

                try
                {
                    FileWrapper fileWrapper = (FileWrapper) formatter.Deserialize(networkStream);
                    FileManager.SaveFile(fileWrapper.Filename, fileWrapper.Data);
                    Console.WriteLine("Receive file: " + fileWrapper.Filename);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                client.Close();
            }).Start();
                
        }
    }
}
