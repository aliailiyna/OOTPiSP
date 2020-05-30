using System;
using System.IO;
using ClassLibraryIPlugin;
using ClassLibraryAttr;

namespace PluginBase64
{
    [Name("Base64")]
    [Extension(".b64")]
    public class Base64 : IPlugin
    {
        public int Encode(string fileNameFrom, string fileNameTo)
        {
            try
            {
                byte[] array;
                using (FileStream fileFrom = new FileStream(fileNameFrom, FileMode.Open))
                {
                    array = new byte[fileFrom.Length];
                    fileFrom.Read(array, 0, array.Length);
                } 

                string strEncoded = Convert.ToBase64String(array);
                array = System.Text.Encoding.Default.GetBytes(strEncoded);

                using (FileStream FileTo = new FileStream(fileNameTo, FileMode.OpenOrCreate))
                {
                    FileTo.Write(array, 0, array.Length);
                }
                return ConstantsPlugins.OK;
            }
            catch
            {
                return ConstantsPlugins.ERROR;
            }
        }
        public int Decode(string fileNameFrom, string fileNameTo)
        {
            try
            {
                byte[] array;
                using (FileStream fileFrom = new FileStream(fileNameFrom, FileMode.Open))
                {
                    array = new byte[fileFrom.Length];
                    fileFrom.Read(array, 0, array.Length);
                }

                string strDecoded = System.Text.Encoding.Default.GetString(array);
                array = Convert.FromBase64String(strDecoded);

                using (FileStream FileTo = new FileStream(fileNameTo, FileMode.OpenOrCreate))
                {
                    FileTo.Write(array, 0, array.Length);
                }
                return ConstantsPlugins.OK;
            }
            catch
            {
                return ConstantsPlugins.ERROR;
            }
        }
    }
}
