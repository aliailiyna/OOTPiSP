using System;
using System.IO;
using ClassLibraryIPlugin;
using ClassLibraryAttr;

namespace PluginBase32
{
    [Name("Base32")]
    [Extension(".b32")]
    public class Base32 : IPlugin
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

                string strEncoded = ConvertBase32.ToBase32String(array);
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
                array = ConvertBase32.FromBase32String(strDecoded);

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
