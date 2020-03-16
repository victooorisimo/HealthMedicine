using System;

namespace HealthMedicine.Models {
    public class Medicine {

        public int idMedicine { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String producer { get; set; }
        public double price { get; set; }
        public int stock { get; set; }

        public Medicine() {

        }

        public static Comparison<Medicine> CompareByName = delegate (Medicine medicine_one, Medicine medicine_two) {
            return medicine_one.name.ToLower().CompareTo(medicine_two.name.ToLower());
        };

    }
}