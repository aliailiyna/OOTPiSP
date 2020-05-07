using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ISerializationClassLibrary
{
    public interface ISerialization
    {
        int Serialize(List<object> list, string fileName);
        int Deserialize(ref List<object> list, string fileName);
    }

    public class Constants
    {
        public const int OK = 0;
        public const int ERROR_SERIALIZATION = 1;
        public const int ERROR_FILE = 2;
        public const int EMPTY_FILE_NAME = 3;
        public const int USER_CANCEL = 4;
        public const int EMPTY_LIST = 5;
    }
}
