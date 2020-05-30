using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryAttr;

namespace ClassLibrary
{
    [Serializable]
    [Name("Письменный стол")]
    public class Desk : Table
    {
        [Name("Наличие компьютерного места")]
        public bool IsComputerPlace;
        [Name("Угловой")]
        public bool IsCorner;

        public Desk()
        {
            IsComputerPlace = false;
            IsCorner = false;
        }
    }
}
