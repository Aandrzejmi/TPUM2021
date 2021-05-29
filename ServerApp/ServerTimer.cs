using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.App
{
    class ServerTimer : IDisposable
    {
        private class Flag { public bool alive = true; }

        private Flag _flag;

        private Thread _thread; 

        public event Action Tick;

        public ServerTimer(int tick_ms)
        {
            var f = new Flag();
            _flag = f;
            _thread = new Thread(() => TimerThread(f, tick_ms));
            _thread.Start();

        }

        private void TimerThread(Flag flag, int tick)
        {
            while (flag.alive)
            {
                Thread.Sleep(tick);

                if (Tick != null)
                    Tick();
            }
        }

        public void Dispose()
        {
            _flag.alive = false;
        }
    }
}
