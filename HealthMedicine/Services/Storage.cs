using CustomGenerics.Structures;
using HealthMedicine.Models;
using System.Collections.Generic;

namespace HealthMedicine.Services{

/*
 * @author: Aylinne Recinos
 * @version: 1.0.0
 * @description: Class for Storage instance
 */
    public class Storage {

        private static Storage _instance = null;

        public static Storage Instance{
            get{
                if (_instance == null) _instance = new Storage();
                return _instance;
            }
        }

        public Order newOrder = new Order();
        public List<Order> orderList = new List<Order>();
        public List<Medicine> medicinesOrder = new List<Medicine>();
        public List<Medicine> medicinesReturn = new List<Medicine>();
        public List<Medicine> medicinesList = new List<Medicine>();
        public AVLStructure<Medicine> avlTree = new AVLStructure<Medicine>();
    }
}