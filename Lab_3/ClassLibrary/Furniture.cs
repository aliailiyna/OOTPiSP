using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Serializable]
    [Name("Мебель")]
    public abstract class Furniture
    {
        [Name("Название")]
        public string Name;

        public Furniture()
        {
            Name = "";
        }
    }
}
