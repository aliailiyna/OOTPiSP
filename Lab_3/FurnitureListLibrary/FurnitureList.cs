using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FurnitureListLibrary
{
    public class FurnitureList
    {
        public Dictionary<Type, Dictionary<string, object>> furniture = new Dictionary<Type, Dictionary<string, object>>();
        public void ClearDictionary()
        {
            foreach (KeyValuePair<Type, Dictionary<string, object>> keyValue in furniture)
            {
                furniture[keyValue.Key].Clear();
            }
        }

        public Boolean IsDictionaryEmpty()
        {
            foreach (KeyValuePair<Type, Dictionary<string, object>> keyValue in furniture)
            {
                if (furniture[keyValue.Key].Count != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public List<object> GetListFromDictionary()
        {
            List<object> list = new List<object>();
            foreach (KeyValuePair<Type, Dictionary<string, object>> firstKeyValue in furniture)
            {
                foreach (KeyValuePair<string, object> secondKeyValue in firstKeyValue.Value)
                {
                    list.Add(secondKeyValue.Value);
                }
            }
            return list;
        }

        public void GetDictionaryFromList(List<object> list)
        {
            foreach (var obj in list)
            {
                FieldInfo field = obj.GetType().GetField("Name");
                String name = (String)field.GetValue(obj);
                Type type = obj.GetType();
                foreach (KeyValuePair<Type, Dictionary<string, object>> keyValue in furniture)
                {
                    if (type.Name == keyValue.Key.Name)
                    {
                        furniture[keyValue.Key].Add(name, obj);
                        break;
                    }
                }
            }
        }
    }
}