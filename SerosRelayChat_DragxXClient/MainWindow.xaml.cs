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
        Socket clientSocket;

        public MainWindow()
        {
            InitializeComponent();

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream , ProtocolType.Tcp);
        }

        private void startConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            clientSocket.BeginConnect(ipEP, new AsyncCallback(onConnect), clientSocket);

            connectionStateLabel.Content = "Verbinde zu " + ipEP.Address + " ...";
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
        /// Methode zur Verbindungsherstellung
        /// </summary>
        /// <param name="aR">AsyncResult der Verbindung</param>
        private void onConnect(IAsyncResult aR)
        {
            Socket server = aR.AsyncState as Socket;

            try
            {
                if (server.Connected)
                {
                    this.Dispatcher.Invoke((Action) delegate() { setConnectionLabel("Verbindung hergestellt"); });
                }
                else
                {
                    this.Dispatcher.Invoke((Action) delegate() { setConnectionLabel("Verbindung fehlgeschlagen"); });
                }
            }
            catch { }
        }
    }
}
