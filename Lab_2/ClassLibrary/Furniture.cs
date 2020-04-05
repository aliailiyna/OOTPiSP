using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
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
