using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteServer
{
    public partial class Form1 : Form
    {
        public Socket clientSocket;
        public string userName;
        public byte[] byteData = new byte[1024];
        private Boolean done;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            done = false;
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                clientSocket.BeginConnect(ipEP, new AsyncCallback(onConnect), clientSocket);
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(onRecieve), clientSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void onConnect(IAsyncResult ar)
        {
            

            byte[] message = new byte[1024];

            Protocol sendingMsg = new Protocol();

            try
            {
                clientSocket.EndConnect(ar);
                sendingMsg.vonUser = "Testclient";
                sendingMsg.Command = "LOGIN";
                sendingMsg.Arg = new String[] { "" };
                message = sendingMsg.ToByte();
                clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnSend), clientSocket);

            }
            catch { }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket server = (Socket)ar.AsyncState;
                server.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //writeError(ex.ToString());
            }
        }

        private void onRecieve(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                Protocol recievedMsg = new Protocol(byteData);

                this.Invoke((Action)delegate() { changeRcdTxtBx(recievedMsg); });
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(onRecieve), clientSocket);
            }
            catch
            {
               
            }
        }

        private void changeRcdTxtBx(Protocol recievedMsg)
        {
            StringBuilder str = new StringBuilder();
            foreach(String s in recievedMsg.Arg)
            {
                str.Append(s + " ");
            }
            this.txt_rcdtext.Text = recievedMsg.vonUser.ToString() + " " + recievedMsg.Command.ToString() + " " + str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Protocol sendMsg = new Protocol();
            Byte[] message = new byte[1024];
            String[] strar = txt_send.Text.Split(' ');
            sendMsg.vonUser = strar[0];
            sendMsg.Command = strar[1];
            StringBuilder str = new StringBuilder();
            for (int i = 2; i < strar.Length - 1; i++)
            {
                str.Append(strar[i]);
                str.Append(" ");
            }
        
            sendMsg.Arg = new String[]{str.ToString()};
            message = sendMsg.ToByte();

            clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnSend), clientSocket);
        }
    }
}
