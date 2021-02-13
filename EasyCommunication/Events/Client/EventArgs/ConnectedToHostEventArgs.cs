﻿using EasyCommunication.Connection;
using System.Net.Sockets;

namespace EasyCommunication.Events.Client.EventArgs
{
    public class ConnectedToHostEventArgs : IClientEventArgs
    {
        public Connection.Connection? Connection { get; internal set; }
        public bool Abort { get; set; }
    }
}
