using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proje_eticaret.App_Classes
{
    public class MyCart
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int OrderID { get; set; }
    }
}