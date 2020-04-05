using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Name("Полка")]
    public class Shelf : FurnitureItem
    {
        [Name("Материал")]
        public string Material;
        [Name("Тип полки")]
        public ShelfType Type;

        public Shelf()
        {
            Material = "";
        }
    }
}
