using HealthMedicine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using HealthMedicine.Services;

namespace HealthMedicine.Controllers
{
    public class MedicineController : Controller
    {
        // GET: Medicine
        public ActionResult Index(FormCollection collection, int? page, string searchMedicine, string quantity, string resupply)
        {
            try
            {
                int pageSize = 5;
                int pageIndex = 1;
                double Total = 0;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                if (!String.IsNullOrEmpty(searchMedicine) && !String.IsNullOrEmpty(quantity))
                {
                    var element = new Medicine
                    {
                        name = collection["searchMedicine"],
                        stock = int.Parse(collection["quantity"])
                    };

                    var found = Storage.Instance.avlTree.searchValue(element, Medicine.CompareByName);
                    var elementToList = from s in Storage.Instance.medicinesList
                                        select s;
                    elementToList = elementToList.Where(s => s.name.Contains(found.name));
                    if (element.stock <= found.stock)
                    {
                        elementToList = elementToList.Where(s => s.name.Contains(found.name));
                        int newValue = Storage.Instance.medicinesList.Find(s => s.name.Contains(found.name)).stock;
                        Storage.Instance.medicinesList.Find(s => s.name.Contains(found.name)).stock = newValue - element.stock;
                        Total = Convert.ToDouble(quantity) * found.price;
                        Storage.Instance.newOrder.Total = +Total;
                        Storage.Instance.medicinesReturn.Add(found);
                        Storage.Instance.medicinesOrder.Add(found);
                        return View(elementToList.ToPagedList(pageIndex, pageSize));
                    }
                }

                if (!String.IsNullOrEmpty(resupply))
                {
                    Resupply();
                }

                Storage.Instance.medicinesReturn.Clear();

                foreach (var item in Storage.Instance.medicinesList)
                {
                    if (item.stock == 0)
                    {
                        Storage.Instance.avlTree.deleteElement(item, Medicine.CompareByName);
                    }
                    else if (item.stock != 0)
                    {
                        Storage.Instance.medicinesReturn.Add(item);
                    }
                }

                IPagedList<Medicine> listMedicines = null;
                List<Medicine> auxiliarMed = new List<Medicine>();
                auxiliarMed = Storage.Instance.medicinesReturn;
                listMedicines = auxiliarMed.ToPagedList(pageIndex, pageSize);
                return View(listMedicines);

            }
            catch (Exception)
            {
                return View();
            }

        }

        //Resupply medicines
        public void Resupply()
        {
            Random rnd = new Random();
            foreach (var item in Storage.Instance.medicinesList)
            {
                if (item.stock == 0)
                {
                    int random = rnd.Next(1, 15);
                    item.stock = random;
                    Storage.Instance.avlTree.addElement(item, Medicine.CompareByName);
                }
            }
        }

        public ActionResult FinalPage()
        {
            return View();
        }
    }
}