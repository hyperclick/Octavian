using System;

namespace Server
{
    public interface IServer : IDisposable
    {
        void Send(string data);
        void SendFile(string v);
    }
}
