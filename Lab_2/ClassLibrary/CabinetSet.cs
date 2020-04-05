using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Name("Набор для кабинета")]
    public class CabinetSet : Furniture
    {
        [Name("Письменный стол")]
        public Desk Desk;
        [Name("Полка")]
        public Shelf Shelf;

        public CabinetSet()
        {
            Desk = new Desk();
            Shelf = new Shelf();
        }
    }
}
