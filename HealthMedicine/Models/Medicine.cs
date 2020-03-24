using HealthMedicine.Services;
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

        public bool saveMedicine(bool structure) {
            if (structure){
                Storage.Instance.avlTree.addElement(this, Medicine.CompareByName);
            }else{
                Storage.Instance.medicinesList.Add(this);
            }
            return true;
        }

        public void saveMedicineAvl(int id, String name){
            Medicine medicine = new Medicine();
            medicine.idMedicine = id;
            medicine.name = name;
            Storage.Instance.avlTree.addElement(medicine, Medicine.CompareByName);
        }

        public bool deleteMedicine() {
            try{
                Storage.Instance.avlTree.deleteElement(this, Medicine.CompareByName);
                return true;
            }
            catch{
                return false;
            }
        }


        public static Comparison<Medicine> CompareByName = delegate (Medicine medicine_one, Medicine medicine_two) {
            return medicine_one.name.ToLower().CompareTo(medicine_two.name.ToLower());
        };

    }
}