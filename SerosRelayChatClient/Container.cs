using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace SerosRelayChatClient
{
    public partial class Container : Form
    {
        public String Username;
        public Socket clientSocket;

        public String Command;
        public String[] Args;
        public byte[] byteData = new byte[1024];

        public Container()
        {
            InitializeComponent();
            
        }

        private void Container_Load(object sender, EventArgs e)
        {
            this.Text = "Seros Relay Chat - Client | Angemeldet als: " + Username;

            byteData = new byte[1024];
            clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(onRecieve), null);            
            
        }

        private void onRecieve(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);
                Protocol recievedMsg = new Protocol(byteData);

                switch (recievedMsg.Command)
                {
                    case "LOGIN":
                        Channel chn = new Channel();
                        chn.MdiParent = this;
                        chn.Text = "Mainchannel";
                        chn.Show();

                        break;
                    default:
                        break;
                }
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

    }

}
