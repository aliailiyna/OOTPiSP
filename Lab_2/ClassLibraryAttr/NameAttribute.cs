using System;

namespace ClassLibraryAttr
{
    [AttributeUsage(AttributeTargets.All)]
    public class NameAttribute : Attribute
    {
        public string Name;
        public NameAttribute(string name)
        {
            this.Name = name;
        }
    }

    public enum FormObjectToDo
    {
        Create, 
        Read, 
        Update
    }
}
