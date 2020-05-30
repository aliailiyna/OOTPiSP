using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Serializable]
    [Name("Элемент мебели")]
    public abstract class FurnitureItem : Furniture
    {
        [Name("Ширина, мм")]
        public ushort Width;
        [Name("Глубина, мм")]
        public ushort Depth;
        [Name("Высота, мм")]
        public ushort Height;

        public FurnitureItem()
        {
            Width = 0;
            Depth = 0;
            Height = 0;
        }
    }
}
