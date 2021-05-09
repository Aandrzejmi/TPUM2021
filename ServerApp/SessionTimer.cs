using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server.App
{
    class SessionTimer
    {
        public event Action OnTimeout;

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
                Counter--;

                if (Counter <= 0)
                {
                    OnTimeout?.Invoke();
                    _thread = null;
                    return;
                }

                Thread.Sleep(1000);
            }
        }
    }
}
