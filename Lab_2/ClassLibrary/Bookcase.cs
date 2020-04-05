using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Name("Книжный шкаф")]
    public class Bookcase : Cabinet
    {
        [Name("Подвижные полки, шт")]
        public byte MovableShelves;
        [Name("Нижние полки, шт")]
        public byte LowerShelves;

        public Bookcase()
        {
            MovableShelves = 0;
            LowerShelves = 0;
        }
    }
}
