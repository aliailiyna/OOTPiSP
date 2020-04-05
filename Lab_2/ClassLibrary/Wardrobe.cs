using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Name("Платяной шкаф")]
    public class Wardrobe : Cabinet
    {
        [Name("Количество секций, шт")]
        public byte NumberOfSections;
        [Name("Количество перекладин, шт")]
        public byte NumberOfCrossBars;
        [Name("Количество ящиков, шт")]
        public byte NumberOfDrawers;
        [Name("Встроенный")]
        public bool IsBuiltIn;

        public Wardrobe()
        {
            NumberOfSections = 0;
            NumberOfCrossBars = 0;
            NumberOfDrawers = 0;
            IsBuiltIn = false;
        }
    }
}
