using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace SerosRelayChatServer
{
    class Client
    {
        private Socket _socket;
        private String _username;
        private List<Channel> Channellist;
        public Socket socket
        {
            get { return this._socket; }
            set { this._socket = value; }
        }
        public String username
        {
            get { return this._username; }
            set { this._username = value; }
        }

        public Client(Socket socket, String username)
        {
            this.socket = socket;
            this.username = username;
        }
    }
}
