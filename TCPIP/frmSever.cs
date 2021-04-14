using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
///microsoft TCP/IP協定
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TCPIP
{
    public partial class frmSever : Form
    {
        private delegate void delUpdateUI(string sMessage);
       
        TcpListener m_server;
        Thread m_thrListening;

        public frmSever()
        {
            InitializeComponent();
            //this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//RED.jfif");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            try 
            {
                int nPort = Convert.ToInt32(txtPort.Text);//設定Port
                IPAddress localAddr = IPAddress.Parse(txtIP.Text);//設定IP

                //Create TcpListener
                m_server = new TcpListener(localAddr, nPort);
                //start listening for client requests
                m_server.Start();
                m_thrListening = new Thread(Listening);
                m_thrListening.Start();
            }
            catch(SocketException ex)
            {
                Console.WriteLine("SocketException:{0}",ex);
            } 
        }

        private void Listening()
        {
            try
            {
                ///buffer for reading data
                byte[] btDatas = new byte[256]; 
                string sData = null;

                while (true)
                {
                    UpdateStatus("Waiting for connection...");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = m_server.AcceptTcpClient(); // 要等有Client建立連線後才會繼續往下執行
                    UpdateStatus("Connect to client!");
                    
                    sData = null;
                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(btDatas, 0, btDatas.Length)) != 0) // 當有資料傳入時將資料顯示至介面上
                    {
                        sData = System.Text.Encoding.ASCII.GetString(btDatas, 0, i);
                        UdpateMessage("Received Data:" + sData);
                        Thread.Sleep(3000);
                    }
                    UdpateMessage("DISCONNECT.....");
                    client.Close();
                    Thread.Sleep(3000);
                }
            }
            catch(SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }         
        }

        private void UpdateStatus(string sStatus)
        {
            if(this.InvokeRequired)
            {
                delUpdateUI del = new delUpdateUI(UpdateStatus);
                this.Invoke(del, sStatus);
            }
            else
            {
                labStatus.Text = sStatus;
                //if (sStatus == "Connect to client!")
                    //this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//Green.jfif");
                //else
                    //this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//RED.jfif");
            }
        }

        private void UdpateMessage(string sReceiveData)
        {
            if (this.InvokeRequired)
            {
                delUpdateUI del = new delUpdateUI(UdpateMessage);
                this.Invoke(del, sReceiveData);
            }
            else
            {
                txtMessage.Text += sReceiveData + Environment.NewLine;
            }
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void labStatus_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///Graphics g = pictureBox1.CreateGraphics();
            ///g.FillEllipse(Brushes.Red,0,0,60,60);
            //this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//RED.jfif");
        }
    }
}
