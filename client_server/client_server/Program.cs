using Server;
using System;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3 || args[1] != "-c")
            {
                ShowUsage();
                return;
            }

            var server = Factory.Create();
            server.Send("Hello World!");
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usasge:");
            Console.WriteLine($"client -c <path to file to send>");
        }
    }
}
