using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerosRelayChatServer
{
    class Channel
    {
        private String _Channelname;
        public List<Client> Clientlist;
        public List<Client> ModList;
        public List<Client> VoiceList;
        public List<Client> OPList;

        public String Channelname
        {
            get { return this._Channelname; }
            set { this._Channelname = value; }
        }

        public Channel(String Channelname)
        {
            this._Channelname = Channelname;
            this.Clientlist = new List<Client>();
            this.ModList = new List<Client>();
            this.VoiceList = new List<Client>();
            this.OPList = new List<Client>();
        }

        public void addClient(Client client)
        {
            Clientlist.Add(client);
        }

        public void removeClient(String Username)
        {
            var Match = Clientlist.First(Client => Client.username == Username);
        }

        public Boolean isClientinChn(String Username)
        {
            var match = Clientlist.First(Client => Client.username == Username);
            if (match != null)
                return true;
            return false;
        }
    }
}
