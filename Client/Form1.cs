using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientServerFileSender;

namespace Client
{
    public partial class Form1 : Form
    {
        private string path;
        private const string DEFAULT_IP = "127.0.0.1";
        private const int DEFAULT_PORT = 11000;


        public Form1()
        {
            InitializeComponent();
            port.Value = DEFAULT_PORT;
            ipAddress.Text = DEFAULT_IP;
        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            path = fileDialog.FileName;
            filePathTextBox.Text = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                new Thread(() =>
                {
                    Client client = new Client(IPAddress.Parse(ipAddress.Text), (int)port.Value);
                    FileWrapper fileWrapper = new FileWrapper();

                    try
                    {
                        fileWrapper.Filename = Path.GetFileName(path);
                        fileWrapper.Data = File.ReadAllBytes(path);
                        client.SendFile(fileWrapper);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }).Start();
            }
        }
    }
}