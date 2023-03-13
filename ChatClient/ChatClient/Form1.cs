using System;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        Client client;

        public Form1(string address, int port)
        {
            InitializeComponent();

            client = new Client(address, port, (message) =>
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => InsertNewMessage(message)));
                }
                else
                {
                    InsertNewMessage(message);
                }
            });
            client.Start();
        }

        private void InsertNewMessage(string msg)
        {
            // Change color of username
            //msgOutputBox.SelectionColor = Color.Blue;
            //msgOutputBox.AppendText("Client: ");
            //msgOutputBox.SelectionColor = msgOutputBox.ForeColor;

            msgOutputBox.AppendText(msg);
            msgOutputBox.AppendText(Environment.NewLine);
            //msgInputBox.Text = ""; // greska
        }

        private void sendMsgBtn_Click(object sender, EventArgs e)
        {
            if (!msgInputBox.Text.Equals(""))
            {
                client.SendMessage(msgInputBox.Text);

                msgInputBox.Text = "";
                
                // scroll to end
                /*msgOutputBox.SelectionStart = msgOutputBox.Text.Length;
                msgOutputBox.ScrollToCaret();*/
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Close();
        }
    }
}
