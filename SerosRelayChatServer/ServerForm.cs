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

namespace SerosRelayChatServer
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        private byte[] byteData = new byte[1024];
        private Socket _serverSocket;

        private List<Client> Clientlist = new List<Client>();
        private List<Channel> ChannelList = new List<Channel>();

        /*
         * kommt ein Client und sendet LOGIN dann wird dieser in die Liste hinzugefügt
         * Wenn der Client eine Nachricht in ein Channel schreibt wird im Channellist überprüft
         * ob der Client dort vorhanden ist.
         * 
         */
        #region Delegates
        delegate void d_writeLog(String text);
        public void writeLog(String text)
        {
            if (this.txt_log.InvokeRequired)
            {
                d_writeLog d = new d_writeLog(writeLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                txt_log.Text += text;
            }
        }
        #endregion


        private void onAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = _serverSocket.EndAccept(ar);
                _serverSocket.BeginAccept(new AsyncCallback(onAccept), null);

                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(onRecieve), clientSocket);
            }
            catch (Exception ex)
            {
                writeError(ex.ToString());
            }
        }

        private void onRecieve(IAsyncResult ar)
        {
            try
            {
        
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                byte[] message;

                Protocol recievedMsg = new Protocol(byteData);
                Protocol sendingMsg = new Protocol();
                

                String Args = ConvertSringArrayToString(recievedMsg.Arg);
                writeLog(recievedMsg.vonUser + " " + recievedMsg.Command + " "  + Args + "\n");
                
                switch (recievedMsg.Command)
                {
                    case "LOGIN":
                        Client client = new Client(clientSocket,recievedMsg.vonUser);
                        Clientlist.Add(client); //Den Client zum Server hinzufügen
                        sendingMsg.vonUser = "System"; 
                        sendingMsg.Command = "LOGIN";
                        sendingMsg.Arg = new String[]{"Willkommen auf Seros Server"}; //ToDo Add Config Server Login Message
                        message = sendingMsg.ToByte();
                        clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnSend), clientSocket);
                        break;
                    case "JOIN":
                        break;
                    case "SEND":
                        break;
                    case "LIST":
                        break;
                    case "WHISPER":
                        break;
                    case "KICK":
                        break;
                    case "BAN":
                        break;
                    case "MOD":
                        break;
                    case "VOICE":
                        break;
                    case "OP":
                        break;
                    case "UNMOD":
                        break;
                    case "DEOP":
                        break;
                    case "CHNMOD":
                        break;
                    case "TOPIC":
                        break;
                    case "PING":
                        break;
                    case "PONG":
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                writeError(ex.ToString());
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                writeError(ex.ToString());
            }
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8000);

                _serverSocket.Bind(ipEndPoint);
                _serverSocket.Listen(4);

                _serverSocket.BeginAccept(new AsyncCallback(onAccept), null);
            }
            catch (Exception ex)
            {
                writeError(ex.ToString());
            }
        }

        #region anderes
        private void writeError(String Error)
        {
            Console.WriteLine(">>   SRC ERROR   <<");
            Console.WriteLine("===================");
            Console.WriteLine(Error.ToString());
            Console.WriteLine("===================");
        }

        private String ConvertSringArrayToString(String[] strArray)
        {
            StringBuilder StrBuilder = new StringBuilder();
            foreach (String str in strArray)
            {
                StrBuilder.Append(str);
                StrBuilder.Append(" ");
            }
            return StrBuilder.ToString();
        }
        #endregion
    }
}
