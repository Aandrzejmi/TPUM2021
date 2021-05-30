using CommunicationAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.App
{
    class Connection
    {
        private static uint counter = 0;

        public readonly uint id;

        public Connection()
        {
            id = counter++;
        }

        public WebSocketConnection ws;
        public SessionTimer timer;
        public SessionTimeoutObserver timeoutObserver;
        public SubscribeUpdateObserver updateObserver;
        public IDisposable updateUnsubscriber;
        public MessageHandler handler;

        public int refreshSubscription = -1;
    }
}
