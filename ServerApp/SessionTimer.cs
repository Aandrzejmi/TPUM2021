using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunicationAPI;

namespace Server.App
{
    class SessionTimer : IObservable<int>
    {
        public readonly int timeout;
        public int Counter { get; private set; }

        public bool Stopped { get; private set; }

        Thread _thread;

        public SessionTimer(int timeout)
        {
            Counter = this.timeout = timeout;
            Stopped = true;
        }

        public void Start()
        {
            if (_thread == null)
            {
                _thread = new Thread(DecrementLoop);
                _thread.Start();
            }


            Stopped = false;
        }

        public void Stop() => Stopped = true;

        public void Reset()
        {
            Counter = timeout;
        }

        private void DecrementLoop()
        {
            while (true)
            {
                while (Stopped) {; }
                
                if (Counter <= 0)
                {
                    Completed();
                    _thread = null;
                    return;
                }

                Next(Counter--);

                Thread.Sleep(1000);
            }
        }

        #region observable

        private void Next(int value)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }

        private void Completed()
        {
            foreach (var observer in _observers.ToArray())
            {
                observer?.OnCompleted();
            }
        }

        private List<IObserver<int>> _observers = new List<IObserver<int>>();

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(this, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private SessionTimer _observable;
            private IObserver<int> _observer;

            public Unsubscriber(SessionTimer observable, IObserver<int> observer)
            {
                _observable = observable;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observable?._observers?.Contains(_observer) ?? false)
                    _observable._observers.Remove(_observer);
            }
        }

        #endregion
    }

    class SessionTimeoutObserver : IObserver<int>
    {
        private readonly Action<string> Log;
        private IDisposable _unsubscriber;
        private WebSocketConnection _ws;

        public SessionTimeoutObserver(WebSocketConnection ws, Action<string> logger)
        {
            _ws = ws;
            Log = logger;
        }

        public void Subscribe(IObservable<int> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public void OnCompleted()
        {
            Task.Run(async() =>
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

        public void OnNext(int value)
        {
            Log($"{value} seconds to timeout");
        }
    }
}
