using CommunicationAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.App
{
    class Connection
    {
        public WebSocketConnection ws;
        public SessionTimer timer;
        public SessionTimeoutObserver observer;
        public MessageHandler handler;
    }
}
