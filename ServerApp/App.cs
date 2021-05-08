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

            try
            {
                using (CommunicationManager node = new CommunicationManager(8081, consoleLogger))
                {
                    await node.InitServerAsync();
                    Console.ReadLine();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception thrown: {e}");
            }
            
        }

        static void ConnectionHandler(CommunicationAPI.WebSocketConnection webSocketConnection)
        {
            webSocketConnection.onMessage = Console.WriteLine;
            webSocketConnection.onClose = () => { Console.WriteLine("Connection closed"); };
            webSocketConnection.onError = () => { Console.WriteLine("Connection error has happened"); };
        }
    }
}
