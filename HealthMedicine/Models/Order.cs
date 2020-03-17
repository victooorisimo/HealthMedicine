using System;
using System.Collections.Generic;
using HealthMedicine.Storage;

namespace HealthMedicine.Models {
    public class Order {

        public static int codeClient = 0;
        public int ClientId { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Nit { get; set; }
        public double Total { get; set; }

        public List<Order> orders = new List<Order>();

        public bool saveOrder()
        {
            try
            {
                codeClient++;
                this.ClientId = codeClient;
                Storage.Storage.Instance.newOrder = this;
                Storage.Storage.Instance.orderList.Add(Storage.Storage.Instance.newOrder);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}