using System;

namespace Server
{
    public interface IServer
    {
        void Send(string data);
        void SendFile(string v);
    }
}
