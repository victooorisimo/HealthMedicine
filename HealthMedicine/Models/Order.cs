using System;
using System.Collections.Generic;

namespace HealthMedicine.Models {
    public class Order {

        public static int codeClient = 0;
        public int ClientId { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Nit { get; set; }
        public double Total { get; set; }

        public List<Order> orders = new List<Order>();
    }
}