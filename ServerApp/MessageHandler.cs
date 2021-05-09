using CommunicationAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.App
{
    class MessageHandler
    {
        private readonly WebSocketConnection socket;
        private readonly Action<string> Log;
        private uint counter = 0;


        public MessageHandler(WebSocketConnection ws, Action<string> logFinction)
        {
            socket = ws;
            Log = logFinction;
        }

        public void Handle(string data)
        {
            Task.Run(async () =>
            {
                uint no = counter++;
                Log($"[Received message {no}]: {data}");

                var split = data.Split();

                switch (split[0])
                {
                    case "send":
                        Log($"[{no} - Send request]: aaa");
                        //Resolve message
                        await socket.SendAsync("HEEEEEEEEEEEEELLLOOOOOO THERE");
                        break;

                    case "add":
                        Log($"[{no} - Add request]: bbb");
                        break;

                    case "update":
                        Log($"[{no} - Update request]: ccc");
                        break;

                    default:
                        Log($"[{no} - Message unknown]: no response");
                        break;
                }
            });
        }
    }
}
