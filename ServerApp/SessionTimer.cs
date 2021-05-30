using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunicationAPI;

namespace Server.App
{
    class SessionTimer : IObservable<SessionTimer.State>
    {
        public struct State
        {
            public int counter;
            public int limit;
        }

        private State CurrentState => new State() { counter = Counter, limit = Limit };

        public readonly int timeout;

        public int Counter { get; private set; }
        public int Limit { get; private set; }
        public bool Stopped { get; private set; }

        Thread _thread;

        public SessionTimer(int timeout)
        {
            Counter = 0;
            Limit = this.timeout = timeout;
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
            Limit = Counter + timeout;
        }

        private void DecrementLoop()
        {
            while (true)
            {
                while (Stopped) {; }
                
                if (Counter > Limit)
                {
                    Completed();
                    _thread = null;
                    return;
                }

                Next();
                Counter++;

                Thread.Sleep(1000);
            }
        }

        #region observable

        private void Next()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(CurrentState);
            }
        }

        private void Completed()
        {
            foreach (var observer in _observers.ToArray())
            {
                observer?.OnCompleted();
            }
        }

        private List<IObserver<SessionTimer.State>> _observers = new List<IObserver<SessionTimer.State>>();

        public IDisposable Subscribe(IObserver<SessionTimer.State> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(this, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private SessionTimer _observable;
            private IObserver<SessionTimer.State> _observer;

            public Unsubscriber(SessionTimer observable, IObserver<SessionTimer.State> observer)
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
}
