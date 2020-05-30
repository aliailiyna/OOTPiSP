using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryAttr
{
    [AttributeUsage(AttributeTargets.All)]
    public class ExtensionAttribute : Attribute
    {
        public string Extension;
        public ExtensionAttribute(string name)
        {
            this.Extension = name;
        }
    }
}
