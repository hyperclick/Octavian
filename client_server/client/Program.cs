using Server;
using System;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2 || args[0] != "-c")
            {
                ShowUsage();
                return;
            }

            var server = Factory.Create();
            server.SendFile(args[1]);
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usasge:");
            Console.WriteLine($"client -c <path to file to send>");
        }
    }
}
