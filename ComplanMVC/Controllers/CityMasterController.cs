using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplanMVC.EntityFrameworkModelClass;

namespace ComplanMVC.Controllers
{
    public class CityMasterController : Controller
    {
        private DBComplianceEntities db = new DBComplianceEntities();

        //
        // GET: /CityMaster/

        public ActionResult Index()
        {
            return View(db.CityMasters.ToList());
        }

        //
        // GET: /CityMaster/Details/5

        public ActionResult Details(int id = 0)
        {
            CityMaster citymaster = db.CityMasters.Find(id);
            if (citymaster == null)
            {
                return HttpNotFound();
            }
            return View(citymaster);
        }

        //
        // GET: /CityMaster/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CityMaster/Create

        [HttpPost]
        public ActionResult Create(CityMaster citymaster)
        {
            if (ModelState.IsValid)
            {
                db.CityMasters.Add(citymaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(citymaster);
        }

        //
        // GET: /CityMaster/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CityMaster citymaster = db.CityMasters.Find(id);
            if (citymaster == null)
            {
                return HttpNotFound();
            }
            return View(citymaster);
        }

        //
        // POST: /CityMaster/Edit/5

        [HttpPost]
        public ActionResult Edit(CityMaster citymaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citymaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(citymaster);
        }

        //
        // GET: /CityMaster/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CityMaster citymaster = db.CityMasters.Find(id);
            if (citymaster == null)
            {
                return HttpNotFound();
            }
            return View(citymaster);
        }

        //
        // POST: /CityMaster/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CityMaster citymaster = db.CityMasters.Find(id);
            db.CityMasters.Remove(citymaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ViewResult Home()
        {
            return View();
        }
    }
}