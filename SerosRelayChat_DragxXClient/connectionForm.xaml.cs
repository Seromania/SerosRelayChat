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
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace SerosRelayChat_DragxXClient
{
    /// <summary>
    /// Interaktionslogik für connectionForm.xaml
    /// </summary>
    public partial class connectionForm : Window
    {
        MainWindow mw;

        public connectionForm(MainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
        }

        private void abortButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            int portNr = 8000;

            try
            {
                portNr = Convert.ToInt32(portTextBox.Text);
            }
            catch { }

            mw.startConnect(ipTextBox.Text, portNr, usernameTextBox.Text);

            this.Close();
        }
    }
}
