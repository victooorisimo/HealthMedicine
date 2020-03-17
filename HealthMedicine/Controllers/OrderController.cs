using HealthMedicine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string path = Server.MapPath($"~/Files/{file.FileName}");
            try{
                using (var fileStream = new FileStream(path, FileMode.Open)){
                    using (var streamReader = new StreamReader(fileStream)){
                        Medicine newMedicine = new Medicine();
                        while (streamReader.Peek() >= 0) {
                            String lineReader = streamReader.ReadLine();
                            String[] parts = lineReader.Split(',');
                            if(parts[0] != ("id")){
                                if (parts.Length == 6){
                                    newMedicine.idMedicine = Convert.ToInt32(parts[0]);
                                    newMedicine.name = parts[1];

                                }else {

                                }
                            }

                        }
                    }

                }
                    return RedirectToAction("Index");
            }catch (Exception e) {
                e.ToString();
                return RedirectToAction("Index");
            }

        }


    }
}
