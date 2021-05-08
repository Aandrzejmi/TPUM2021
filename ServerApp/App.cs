using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.App
{
    public class App
    {
        static async Task Main(string [] args)
        {
            Action<string> consoleLogger = Console.WriteLine;

            await SharedData.WebSocketServer.Server(8081, ConnectionHandler);

        }

        static void ConnectionHandler(SharedData.WebSocketConnection webSocketConnection)
        {
            webSocketConnection.onMessage = Console.WriteLine;
            webSocketConnection.onClose = () => { Console.WriteLine("Connection closed"); };
            webSocketConnection.onError = () => { Console.WriteLine("Connection error has happened"); };
        }
    }
}
