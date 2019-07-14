using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class CustomerAddController : Controller
    {
        //
        // GET: /CustomerAdd/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult InsertCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertCustomer(Customer obcustomer)
        {
            obcustomer.Birthdate = Convert.ToDateTime(obcustomer.Birthdate);
            if (ModelState.IsValid) //checking model is valid or not
            {
                DataAccessLayer.DBdata obdb = new DataAccessLayer.DBdata();
                string strinsert = "";
                strinsert = obdb.InsertData(obcustomer);
                ViewData["InsertResult"] = strinsert;
                ModelState.Clear(); // Clean model
                return View();
            }
            else
            {
                ModelState.AddModelError("","Error in saving data");
                return View();
            }
        }
        [HttpGet]
        public ActionResult ShowAllCustomerDetails()
        {
            ViewBag.Message = "Customer data (by VIEWDATA)";
            ViewData["CustomerDisplay"] = new DataAccessLayer.DBdata().Selectalldata();
            return View();
        }
        [HttpGet]
        public ActionResult EditData(string id)
        {
            DataAccessLayer.DBdata obdb = new DataAccessLayer.DBdata();
            return View(obdb.SelectDatabyID(id));
        }
        [HttpPost]
        public ActionResult EditData(Customer obcustomer)
        {
            obcustomer.Birthdate = Convert.ToDateTime(obcustomer.Birthdate);
            if (ModelState.IsValid) //checking model is valid or not
            {
                DataAccessLayer.DBdata obdb = new DataAccessLayer.DBdata();
                string strinsert = "";
                strinsert = obdb.UpdateData(obcustomer);
                ViewData["UpdateResult"] = strinsert;
                ModelState.Clear(); // Clean model
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }
    }
}
