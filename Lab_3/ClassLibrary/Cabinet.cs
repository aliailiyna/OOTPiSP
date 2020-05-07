using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Serializable]
    [Name("Шкаф")]
    public abstract class Cabinet : FurnitureItem
    {
        [Name("Материал корпуса")]
        public string BodyMaterial;
        [Name("Материал фасада")]
        public string FacadMaterial;

        public Cabinet()
        {
            BodyMaterial = "";
            FacadMaterial = "";
        }
    }
}
