using System;
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
    }
}
