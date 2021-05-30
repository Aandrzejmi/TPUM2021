using CommunicationAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.App
{
    class SessionTimeoutObserver : IObserver<SessionTimer.State>
    {
        private readonly Action<string> Log;
        private IDisposable _unsubscriber;
        private WebSocketConnection _ws;

        public SessionTimeoutObserver(WebSocketConnection ws, Action<string> logger)
        {
            _ws = ws;
            Log = logger;
        }

        public void Subscribe(IObservable<SessionTimer.State> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public void OnCompleted()
        {
            Task.Run(async () =>
            {
                var task = _ws.DisconnectAsync();
                Log($"timeout!");
                Unsubscribe();
                await task;
            });
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(SessionTimer.State value)
        {
            Log($"{value.limit - value.counter} seconds to timeout");
        }
    }
}
