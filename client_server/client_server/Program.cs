using Server;
using System;

namespace client_server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = Factory.Create();
            server.Send("Hello World!");
        }
    }
}
