using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Name("Обеденный стол")]
    public class DiningTable : Table
    {
        [Name("Форма стола")]
        public TableShapeType Shape;
    }
}
