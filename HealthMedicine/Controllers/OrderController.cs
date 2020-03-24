﻿using HealthMedicine.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace HealthMedicine.Controllers {
    public class OrderController : Controller {
        // GET: Order
        public ActionResult Index() {
            Order order = new Order();
            return View(order.orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        [HttpPost]
        public ActionResult LoadDocument(HttpPostedFileBase file){
            try{
                var ubication = Server.MapPath($"~/Test/{file.FileName}");
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
                return RedirectToAction("Index");
            }catch (Exception e){
                e.ToString();
                return RedirectToAction("Index");
            }

        }


    }
}
