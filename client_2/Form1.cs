using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using System.Net.Sockets;
using System.Threading;

namespace client2
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readdate = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientSocket.Connect(textBox1.Text, Int32.Parse(textBox2.Text));
            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }
        private void getMessage()
        {
            string returndate;
            while (true)
            {
                serverStream = clientSocket.GetStream();
                var buffsize = clientSocket.ReceiveBufferSize;
                byte[] instream = new byte[buffsize];

                serverStream.Read(instream, 0, buffsize);
                returndate = System.Text.Encoding.ASCII.GetString(instream);

                readdate = returndate;
                msg();

            }
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                textBox4.Text = readdate;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] outstream = Encoding.ASCII.GetBytes(textBox3.Text);

            serverStream.Write(outstream, 0, outstream.Length);
            serverStream.Flush();
            textBox3.Clear();
        }


    }
}
