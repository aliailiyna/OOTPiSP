using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Name("Стол")]
    public abstract class Table : FurnitureItem
    {
        [Name("Материал опор")]
        public string SupportMaterial;
        [Name("Материал столешницы")]
        public string CountertopMaterial;

        public Table()
        {
            SupportMaterial = "";
            CountertopMaterial = "";
        }
    }
}
