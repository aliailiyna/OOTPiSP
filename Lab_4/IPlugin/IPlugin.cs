using System;
using System.Collections.Generic;

namespace ClassLibraryIPlugin
{
    public interface IPlugin
    {
        int Encode(string fileNameFrom, string fileNameTo);
        int Decode(string fileNameFrom, string fileNameTo);
    }

    public class ConstantsPlugins
    {
        public const int OK = 0;
        public const int ERROR = 1;
    }
}
