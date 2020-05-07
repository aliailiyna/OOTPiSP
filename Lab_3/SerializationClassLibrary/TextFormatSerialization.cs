using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryAttr;
using ISerializationClassLibrary;

namespace SerializationClassLibrary
{
    [Name("Сериализация в текстовом формате")]
    class TextSerialization : ISerialization
    {
        static Assembly classesAssembly;
        static List<Type> classesTypesLibrary = new List<Type>();

        private static void WriteFields(object currentObject, StreamWriter sw)
        {
            FieldInfo[] fields = currentObject.GetType().GetFields();

            foreach (FieldInfo field in fields)
            {
                sw.WriteLine(field.Name + ": " + field.GetValue(currentObject));
                if ((field.FieldType.IsClass) && (field.FieldType != typeof(String)))
                {
                    WriteFields(field.GetValue(currentObject), sw);
                }
            }
        }

        private static object ReadFields(Type currentClass, StreamReader sr)
        {
            var obj = classesAssembly.CreateInstance(currentClass.FullName);
            FieldInfo[] fields = currentClass.GetFields();
            foreach (FieldInfo field in fields)
            {
                string info = sr.ReadLine();
                string value = info.Substring(field.Name.Length + ": ".Length);
                if (field.FieldType.IsPrimitive)
                {
                    if (field.FieldType == typeof(ushort))
                    {
                        field.SetValue(obj, ushort.Parse(value));
                    }
                    if (field.FieldType == typeof(byte))
                    {
                        field.SetValue(obj, byte.Parse(value));
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        field.SetValue(obj, bool.Parse(value));
                    }
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(obj, value);
                }
                else if (field.FieldType.IsEnum)
                {
                    foreach (var enumItem in field.FieldType.GetEnumValues())
                    {
                        var enumName = enumItem.ToString();
                        if (enumName == value)
                        {
                            field.SetValue(obj, enumItem);
                            break;
                        }
                    }
                }
                else if (field.FieldType.IsClass)
                {
                    Object agreg = ReadFields(field.FieldType, sr);
                    field.SetValue(obj, agreg);
                }
            }
            return obj;
        }

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
                            string objectType = obj.GetType().FullName;
                            sw.WriteLine(objectType);
                            WriteFields(obj, sw);
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
                        classesAssembly = Assembly.LoadFile(@"D:\OOTPiSP\Lab_3\ClassLibrary\bin\Debug\netstandard2.0\ClassLibrary.dll");
                        List<Type> classesTypesLibraryHelp = classesAssembly.GetTypes().ToList();
                        classesTypesLibrary = new List<Type>();
                        foreach (var type in classesTypesLibraryHelp)
                        {
                            if (type.IsClass)
                            {
                                classesTypesLibrary.Add(type);
                            }
                        }
                        using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default))
                        {
                            while (!sr.EndOfStream)
                            {
                                string info = sr.ReadLine();
                                foreach (var type in classesTypesLibrary)
                                {
                                    if (type.FullName == info)
                                    {
                                        Type objectType = type;
                                        object obj = ReadFields(objectType, sr);
                                        list.Add(obj);
                                        break;
                                    }
                                }
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
