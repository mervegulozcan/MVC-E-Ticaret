using System;

namespace proje_eticaret.App_Classes
{
    public class OrderList
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerUserName { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipperCompanyName { get; set; }
        public DateTime ShipperDate { get; set; }
        public int TrackingNo { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}