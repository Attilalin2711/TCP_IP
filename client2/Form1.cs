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
            this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//RED.jfif");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                clientSocket.Connect(textBox1.Text, Int32.Parse(textBox2.Text));
                this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//GREEN.jfif");
                Thread ctThread = new Thread(getMessage);
                ctThread.Start();
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
        }
        private void getMessage()
        {
            try
            {
                string returndate;
                var buffsize = clientSocket.ReceiveBufferSize;
                byte[] instream = new byte[buffsize];

                while (true)
                {
                    serverStream = clientSocket.GetStream();
                    //MessageBox.Show("receive");
                    int i;
                    while((i = serverStream.Read(instream, 0, buffsize))!=0)
                    {
                        returndate = System.Text.Encoding.ASCII.GetString(instream);
                        // MessageBox.Show(returndate);
                        readdate = returndate;
                        //MessageBox.Show(readdate);
                        if (readdate != "0")
                        {
                            this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//RED.jfif");
                            //MessageBox.Show("RED");
                        }
                        Thread.Sleep(3000);
                        this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//GREEN.jfif");
                        //MessageBox.Show("GREEN");
                        msg();
                    }
                    clientSocket.Close();
                    Thread.Sleep(5);
                 }      
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }         
        }

        private void msg()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                //textBox4.Text = readdate;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] outstream = Encoding.ASCII.GetBytes(textBox3.Text);
            
            serverStream.Write(outstream,0,outstream.Length);
            serverStream.Flush();
            textBox3.Clear();
        }

    }
}
