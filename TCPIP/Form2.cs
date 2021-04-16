using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPIP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            timer1.Enabled = false;
            timer1.Interval = 500;
            timer1.Start();
            
        }
        public Form2(string str)
        {
            InitializeComponent();
            timer1.Enabled = false;
            timer1.Interval = 3000;
            timer1.Start();
            label1.Text = str;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
