using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using ClassLibraryAttr;
using ISerializationClassLibrary;

namespace SerializationClassLibrary
{
    [Name("JSON сериализация")]
    class JsonSerialization : ISerialization
    {
        public int Serialize(List<object> list, string fileName)
        {

            if (fileName != "")
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.Default))
                    {
                        foreach (var obj in list)
                        {
                            var settings = new JsonSerializerSettings()
                            {
                                TypeNameHandling = TypeNameHandling.All
                            };
                            var jsonString = JsonConvert.SerializeObject(obj, settings);
                            sw.WriteLine(jsonString);
                        }

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
                        using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default))
                        {
                            while (!sr.EndOfStream)
                            {
                                var settings = new JsonSerializerSettings()
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                };
                                string jsonString = sr.ReadLine();
                                var obj = JsonConvert.DeserializeObject(jsonString, settings);
                                list.Add(obj);
                            }
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
