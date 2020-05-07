using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryAttr;
using ISerializationClassLibrary;

namespace SerializationClassLibrary
{
    [Name("Бинарная сериализация")]
    public class BinarySerialization : ISerialization
    {
        public int Serialize(List<object> list, string fileName)
        {
            if (fileName != "")
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream objectsFile = new FileStream(fileName, FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(objectsFile, list);
                    }
                    return Constants.OK;
                }
                catch
                {
                    return Constants.ERROR_SERIALIZATION;
                }
            }
            else
            {
                return Constants.EMPTY_FILE_NAME;
            }
        }

        public int Deserialize(ref List<object> list, string fileName)
        {
            if (fileName != "")
            {
                if (File.Exists(fileName))
                {
                    try
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        using (FileStream fs = new FileStream(fileName, FileMode.Open))
                        {
                            list = (List<object>)formatter.Deserialize(fs);
                        }
                        return Constants.OK;
                    }
                    catch
                    {
                        return Constants.ERROR_SERIALIZATION;
                    }
                }
                else
                {
                    return Constants.ERROR_FILE;
                }
            }
            else
            {
                return Constants.EMPTY_FILE_NAME;
            }
        }
    }
}

