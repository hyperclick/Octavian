using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Config
    {
        public string Folder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
    }
}
