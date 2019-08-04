using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public interface IConnector
    {
        IServer Connect(string client_id, Dictionary<string, string> properties);
    }
}
