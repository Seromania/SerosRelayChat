using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace SerosRelayChat_DragxXClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Socket clientSocket;
        public string userName;
        public byte[] byteData = new byte[1024];

        public MainWindow()
        {
            InitializeComponent();

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream , ProtocolType.Tcp);
        }

        private void startConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            connectionForm cF = new connectionForm(this);
            cF.Owner = this;
            cF.ShowDialog();

            chatLog.Text = "";
        }

        public void startConnect(string ip, int port, string username)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse(ip), port);
                clientSocket.BeginConnect(ipEP, new AsyncCallback(onConnect), clientSocket);

                connectionStateLabel.Content = "Verbinde zu " + ipEP.Address + " ...";

                this.userName = username;
            }
            catch
            {
                MessageBox.Show("Fehlerhafte angaben");
            }
        }

        /// <summary>
        /// Ermöglicht das ändern des Status Labels
        /// </summary>
        /// <param name="text">Zu ändender Text</param>
        public void setConnectionLabel(string text)
        {
            connectionStateLabel.Content = text;
        }

        /// <summary>
        /// Neue Chatlog Meldung
        /// </summary>
        /// <param name="text">Zu ändender Text</param>
        public void addChatLogMessage(string userName, string command, string[] args)
        {            
            chatLog.Text += userName + ": ";
            for (int i = 0; i < args.Length; i++)
            {
                if (!args[i].StartsWith("\r\n"))
                {
                    chatLog.Text += args[i] + " ";
                }
                else
                {
                    break;
                }
            }
        }

        public void addChatLogMessage(string text)
        {
            chatLog.Text += text.Substring(0, text.IndexOf("\r\n"));
        }

        /// <summary>
        /// Methode zur Verbindungsherstellung
        /// </summary>
        /// <param name="aR">AsyncResult der Verbindung</param>
        private void onConnect(IAsyncResult aR)
        {
            try
            {
                Socket server = aR.AsyncState as Socket;
                server.EndConnect(aR);

                byte[] message = new byte[1024];
            
                Protocol sendingMsg = new Protocol();         

                if (server.Connected)
                {                   
                    sendingMsg.vonUser = userName;
                    sendingMsg.Command = "LOGIN";
                    sendingMsg.Arg = new String[] { "" };
                    message = sendingMsg.ToByte();
                    server.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnSend), server);                    

                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(onRecieve), clientSocket);

                    this.Dispatcher.Invoke((Action)delegate() { setConnectionLabel("Verbindung hergestellt"); });
                }
                else
                {
                    this.Dispatcher.Invoke((Action) delegate() { setConnectionLabel("Verbindung fehlgeschlagen"); });
                }
            }
            catch 
            {
                this.Dispatcher.Invoke((Action)delegate() { addChatLogMessage("Verbindungsfehler"); });
                this.Dispatcher.Invoke((Action)delegate() { setConnectionLabel("Verbindungsfehler"); });
            }
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

                switch (recievedMsg.Command)
                {
                    case "LOGIN":
                        this.Dispatcher.Invoke((Action)delegate() { addChatLogMessage(">> " + ConvertSringArrayToString(recievedMsg.Arg)); });
                        break;
                    case "SEND":
                        this.Dispatcher.Invoke((Action)delegate() { addChatLogMessage(recievedMsg.vonUser, recievedMsg.Command, recievedMsg.Arg); });
                        break;
                }               

                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(onRecieve), clientSocket);
            }
            catch
            {
                this.Dispatcher.Invoke((Action)delegate() { clientSocket = null; });
                this.Dispatcher.Invoke((Action)delegate() { addChatLogMessage("Verbindung verloren"); });
                this.Dispatcher.Invoke((Action)delegate() { setConnectionLabel("Verbindung verloren"); });
            }
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

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] splittedInput = chatMessageBox.Text.Split(' ');
                byte[] message;

                Protocol sendMessage = new Protocol();
                sendMessage.vonUser = userName;
                sendMessage.Command = splittedInput[0];
                sendMessage.Arg = splittedInput.Skip(1).ToArray();

                message = sendMessage.ToByte();

                clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnSend), clientSocket);
            }
            catch
            {
                this.Dispatcher.Invoke((Action)delegate() { addChatLogMessage("Verbindungsfehler"); });
                this.Dispatcher.Invoke((Action)delegate() { setConnectionLabel("Verbindungsfehler"); });
            }
        }
    }
}
