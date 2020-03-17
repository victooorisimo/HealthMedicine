using HealthMedicine.Storage;
using HealthMedicine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;

namespace HealthMedicine.Controllers
{
    public class MedicineController : Controller
    {
        // GET: Medicine
        public ActionResult Index(FormCollection collection, int? page, string searchMedicine, string quantity, string resupply)
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
                var found = Storage.Storage.Instance.avlTree(element, Medicine.CompareByName); //Add Search method
                var elementToList = from s in Storage.Storage.Instance.medicinesList
                                    select s;
                elementToList = elementToList.Where(s => s.name.Contains(found.name));
                if (element.stock <= found.stock)
                {
                    elementToList = elementToList.Where(s => s.name.Contains(found.name));
                    int newValue = Storage.Storage.Instance.medicinesList.Find(s => s.name.Contains(found.name)).stock;
                    Storage.Storage.Instance.medicinesList.Find(s => s.name.Contains(found.name)).stock = newValue - element.stock;
                    Total = Convert.ToDouble(quantity) * found.price;
                    Storage.Storage.Instance.newOrder.Total = +Total;
                    return View(elementToList.ToPagedList(pageIndex, pageSize));
                }
            }

            if (!String.IsNullOrEmpty(resupply))
            {
                Resupply();
            }

            Storage.Storage.Instance.medicinesReturn.Clear();

            foreach (var item in Storage.Storage.Instance.medicinesList)
            {
                if (item.stock == 0)
                {
                    Storage.Storage.Instance.avlTree.deleteElement(item, Medicine.CompareByName);
                }
                else if (item.stock != 0)
                {
                    Storage.Storage.Instance.medicinesReturn.Add(item);
                }
            }

            IPagedList<Medicine> listMedicines = null;
            List<Medicine> auxiliarMed = new List<Medicine>();
            auxiliarMed = Storage.Storage.Instance.medicinesReturn;
            listMedicines = auxiliarMed.ToPagedList(pageIndex, pageSize);
            return View(listMedicines);
        }

        //Resupply medicines
        public void Resupply()
        {
            Random rnd = new Random();
            foreach (var item in Storage.Storage.Instance.medicinesList)
            {
                if (item.stock == 0)
                {
                    int random = rnd.Next(1, 15);
                    item.stock = random;
                    Storage.Storage.Instance.avlTree.addElement(item, Medicine.CompareByName);
                }
            }
        }

        // GET: Medicine/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Medicine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicine/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Medicine/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Medicine/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Medicine/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Medicine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
