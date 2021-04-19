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
        private delegate void safeCallDelegate(int sec);
        TcpListener m_server;
        Thread m_thrListening;
        NetworkStream stream = default(NetworkStream);
        TcpClient client;
        int delaytime;

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
                    //AcceptTcpClient 這是一種封鎖方法，會傳回 TcpClient 您可用來傳送和接收資料的
                    // You could also use server.AcceptSocket() here.
                    client = m_server.AcceptTcpClient(); // 要等有Client建立連線後才會繼續往下執行
                    UpdateStatus("Connect to client!");
                    
                    sData = null;
                    // Get a stream object for reading and writing
                    // 傳回用來傳送和接收資料的 NetworkStream
                    stream = client.GetStream();
                   
                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(btDatas, 0, btDatas.Length)) != 0) // 當有資料傳入時將資料顯示至介面上
                    {
                        sData = System.Text.Encoding.ASCII.GetString(btDatas, 0, i);
                        startcount(3);
                        UdpateMessage("Received Data:" + sData);
                        //delay(30);
                        Thread.Sleep(50);
                    }
                    UdpateMessage("DISCONNECT.....");
                    client.Close();
                    Thread.Sleep(50);
                }
            }
            catch(SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }         
        }
        public void delay(int sec)
        {
            byte[] outstream = Encoding.ASCII.GetBytes("1");
            stream.Write(outstream, 0, outstream.Length);
            stream.Flush();
            
            //stream.Close();
            //Thread.Sleep(500);
            //outstream = Encoding.ASCII.GetBytes("0");
            //stream.Write(outstream, 0, outstream.Length);
            //stream.Flush();
           
        }
        private void UpdateStatus(string sStatus)
        { 
            //判斷這個物件是否在同一個執行緒上
            if(this.InvokeRequired)
            {
                //當InvokeRequired為true時，表示在不同的執行緒上，所以進行委派的動作
                delUpdateUI del = new delUpdateUI(UpdateStatus);
                this.Invoke(del, sStatus);
                
            }
            else
            {
                //表示在同一個執行緒上了，所以可以正常的呼叫到這個labstatus物件
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
                Form2 form2 = new Form2(sReceiveData);
                delay(0);
                //startcount(3);
                form2.ShowDialog();

                /*
                delUpdateUI del = new delUpdateUI(UdpateMessage);
                this.Invoke(del, sReceiveData);
                ///MessageBox.Show(sReceiveData);
                 */
            }
            else
            {
                Form2 form2 = new Form2();
                form2.ShowDialog();
                /*
                txtMessage.Text += sReceiveData + Environment.NewLine;
                ///MessageBox.Show(sReceiveData);
                 */
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
        public void startcount(int sec)
        {
            if (label5.InvokeRequired)
            {
                var d = new safeCallDelegate(startcount);
                label5.Invoke(d, new object[] {sec});
                //label5.Text = sec + " seconds";
                //MessageBox.Show("invoke=True");
                delaytime = sec;
                timer1.Start();
            }
            else
            {
                label5.Text = sec + " seconds";
                //MessageBox.Show("invoke=FALSE");
                delaytime = sec;
                timer1.Start();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (delaytime > 0)
            {
                delaytime = delaytime - 1;
                label5.Text = delaytime + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                label5.Text = (" ");
            }
        }
            
        private void frmSever_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            startcount(3);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
