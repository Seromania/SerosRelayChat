﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerosRelayChatServer
{
    class Channel
    {
        private String _Channelname;
        private List<Client> Clientlist;
        public String Channelname
        {
            get { return this._Channelname; }
            set { this._Channelname = value; }
        }

        public Channel(String Channelname)
        {
            this._Channelname = Channelname;
        }
    }
}
