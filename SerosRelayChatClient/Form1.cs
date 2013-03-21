using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace SerosRelayChatClient
{
    public partial class Form1 : Form
    {
        public Socket clientSocket;
        public String Username;
        public Boolean done;

        public Form1()
        {
            InitializeComponent();
            done = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_IP.Text.Trim() != "" || txt_Port.Text.Trim() != "" || txt_Username.Text.Trim() != "")
            {
                run();
            }
            else
            {
                MessageBox.Show("Bitte alle Felder eingeben!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void run()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAdress = IPAddress.Parse(txt_IP.Text);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, Convert.ToInt32(txt_Port.Text));

                clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(onConnect), null);
            }
            catch (Exception ex)
            {
                MessageError(ex.ToString());
            }
        }

        private void MessageError(String ex)
        {
            MessageBox.Show(ex, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void onConnect(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
                Protocol sendMsg = new Protocol();
                sendMsg.vonUser = txt_Username.Text;
                sendMsg.Command = "LOGIN";
                sendMsg.Arg = new String[] { "" } ;
                byte[] b = sendMsg.ToByte();

                clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(onSend), null);
            }
            catch (Exception ex)
            {
                MessageError(ex.ToString());
            }
        }

        private void onSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
                this.Username = txt_Username.Text;
                done = true;
                Close();

            }
            catch (Exception ex)
            {
                MessageError(ex.ToString());
            }
        }
    }
}
