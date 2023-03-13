using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string address = textBox1.Text;
            int port = Convert.ToInt32(textBox2.Text);

            var chat = new Form1(address, port);
            chat.Show();

            //this.Close();
        }
    }
}
