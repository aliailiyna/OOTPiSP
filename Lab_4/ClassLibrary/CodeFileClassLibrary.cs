using System;
using ClassLibraryAttr;

namespace ClassLibrary
{
    public enum TableShapeType
    {
        [Name("Круглый")]
        Round,

        [Name("Овальный")]
        Oval,

        [Name("Квадратный")]
        Square,

        [Name("Прямоугольный")]
        Rectangular
    }

    public enum ShelfType
    {
        [Name("Полка")]
        WallShelf,

        [Name("Стеллаж")]
        FloorShelf
    }
}