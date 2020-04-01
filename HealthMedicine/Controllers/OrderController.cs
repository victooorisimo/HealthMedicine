using HealthMedicine.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using HealthMedicine.Services;
using PagedList;
using System.Linq;

namespace HealthMedicine.Controllers {
    public class OrderController : Controller {
        // GET: Order
        public ActionResult Index(string directory) {
            if (Storage.Instance.orderList.Count < 1) {
                Storage.Instance.orderList.Clear();
                Storage.Instance.medicinesOrder.Clear();
            }
            var orderList = Storage.Instance.orderList;

            return View(orderList.ToList());
        }

        // GET: Order/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try{
                var newOrder = new Order
                {
                    Name = collection["Name"],
                    Address = collection["Address"],
                    Nit = collection["Nit"]
                };
                newOrder.saveOrder();
                return RedirectToAction("Index", "Medicine");
            }
            catch{
                return View();
            }
        }

        public ActionResult InitialPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InitialPage(string add)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult LoadDocument(HttpPostedFileBase file){
            try{
                var ubication = Server.MapPath($"~/Test/{file.FileName}");
                int id=0;
                file.SaveAs(ubication);
                using (var fileStream = new FileStream(ubication, FileMode.Open)){
                    using (var streamReader = new StreamReader(fileStream)){
                        Medicine newMedicine;
                        while (streamReader.Peek() >= 0){
                            newMedicine = new Medicine();
                            String lineReader = streamReader.ReadLine();
                            String[] parts = lineReader.Split(',');
                            if (parts[0] != ("id")){
                                if (parts.Length == 6) {
                                    newMedicine.saveMedicineAvl(Convert.ToInt32(parts[0]), parts[1]);
                                    id += 1;
                                    newMedicine.idMedicine = id;
                                    newMedicine.name = parts[1];
                                    newMedicine.description = parts[2];
                                    newMedicine.producer = parts[3];
                                    newMedicine.stock = Convert.ToInt32(parts[parts.Length - 1]);
                                    newMedicine.price = Convert.ToDouble((parts[parts.Length - 2]).Substring(1,
                                        (parts[parts.Length - 2].Length) - 1));
                                    newMedicine.saveMedicine(false);
                                }else {
                                    String data = "";
                                    for (int i = 0; i < parts.Length; i++)
                                    {
                                        if ((parts[0] != parts[i]))
                                        {
                                            if (parts[parts.Length - 1] != parts[i])
                                            {
                                                if (parts[parts.Length - 2] != parts[i])
                                                {
                                                    data = data + parts[i];
                                                }
                                            }
                                        }
                                    }
                                    String[] recolection = data.Split('"');
                                    int module = 0;
                                    for (int j = 0; j < recolection.Length; j++)
                                    {
                                        if (recolection[j] != "")
                                        {
                                            if (module == 0)
                                            {
                                                newMedicine.saveMedicineAvl(Convert.ToInt32(parts[0]), recolection[j]);
                                                id += 1;
                                                newMedicine.idMedicine = id;
                                                newMedicine.name = parts[1];
                                                newMedicine.stock = Convert.ToInt32(parts[parts.Length - 1]);
                                                newMedicine.price = Convert.ToDouble((parts[parts.Length - 2]).Substring(1,
                                                    (parts[parts.Length - 2].Length) - 1));
                                                module++;

                                            }
                                            else if (module == 1)
                                            {
                                                newMedicine.description = recolection[j];
                                                module++;
                                            }
                                            else
                                            {
                                                newMedicine.producer = recolection[j];
                                            }
                                        }
                                    }
                                    newMedicine.saveMedicine(false);
                                }
                            }
                        
                        }
                    }

                }
                return RedirectToAction("Create");
            }catch (Exception e){
                e.ToString();
                return RedirectToAction("Index");
            }

        }
    }
}
