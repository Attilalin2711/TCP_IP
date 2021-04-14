using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
///
using System.Net.Sockets;
using System.Net;



namespace Client
{
    public partial class Form1 : Form
    {
        private TcpClient m_client;
        private delegate void delUpdateUI(string sMessage);

        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.Image = Image.FromFile("C://Users//david2711//Desktop//RED.jfif");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            //UdpateMessage("連線中.....");
            try
            {
                // Create Tcp client.
                int nPort = 13000;
                m_client = new TcpClient("127.0.0.1", nPort);
                //UdpateMessage("已連接. .......");
                //UdpateMessage("請輸入要傳送的字符串");
            }
            catch (ArgumentNullException a)
            {
                Console.WriteLine("ArgumentNullException:{0}", a);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException:{0}", ex);
            }
        }

        private void btnDisconnect_Click_1(object sender, EventArgs e)
        {
            m_client.Close();
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
                txtData.Text += sReceiveData + Environment.NewLine;
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] btData = System.Text.Encoding.ASCII.GetBytes(txtData.Text); // Convert string to byte array.
            try
            {
                NetworkStream stream = m_client.GetStream();
                stream.Write(btData, 0, btData.Length); // Write data to server.
                txtData.Clear(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Write Exception:{0}", ex);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
