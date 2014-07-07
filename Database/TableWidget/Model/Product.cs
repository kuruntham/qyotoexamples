using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableWidget.Model
{   
    class Product
    {
        public Int64 Id{ get; set; }
        public string Code{ get; set; }
        public string Name{ get; set; }
        public string Unit{ get; set; }       
        public float UnitPrice{ get; set; }
    }
}
