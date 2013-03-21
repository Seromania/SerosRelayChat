using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SerosRelayChatClient
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 loginForm = new Form1();

            Application.Run(loginForm);
            if (loginForm.done == true)
            {
                Container con = new Container();
                con.clientSocket = loginForm.clientSocket;
                con.Username = loginForm.Username;
                con.ShowDialog();
            }
            
        }
    }
}
