using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class ApplicationMasController : Controller
    {
        //
        // GET: /ApplicationMas/

        public ActionResult Index()
        {
            Application obj = TempData["Page1"] as Application;
            if (TempData["Page1"] == null)
            {
                TempData["Name"] = "";
                TempData["Address"] = "";
                TempData["Mobileno"] = "";
                TempData["Birthdate"] = "";
                TempData["Mobileno"] = "";
                TempData["EmailID"] = "";
            }
            else
            {
                TempData["Name"] = obj.Name;
                TempData["Address"] = obj.Address;
                TempData["Mobileno"] = obj.Mobileno;
                TempData["Birthdate"] = obj.Birthdate;
                TempData["Mobileno"] = obj.Mobileno;
                TempData["EmailID"] = obj.EmailID;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(Application obj)
        {
            TempData["Page1"] = obj;
            //TempData.Keep(key: "Emp");
            if (TempData["Page2"] != null)
            {
                Session["holdTempdata2"] = TempData["Page2"];
            }
            return Redirect("/ApplicationMas/Index1");
        }

        //public ActionResult Index1()
        //{
        //    Application emp = TempData["Page1"] as Application;
        //    return View(emp);  
        //}

        [HttpGet]
        public ActionResult Index1()
        {
            /* Application obj = TempData["Page1"] as Application;
             string Username1 = Convert.ToString(obj.Name);
             string Username = Convert.ToString(TempData["Name"]);*/
            Session["holdTempdata1"] = TempData["Page1"];
            Application obj = TempData["Page2"] as Application;
            if (TempData["Page2"] == null)
            {
                TempData["PANNO"] = "";
                TempData["ADHARNO"] = "";
                TempData["PASSPORTNO"] = "";
            }
            else
            {
                TempData["PANNO"] = obj.PANNO;
                TempData["ADHARNO"] = obj.ADHARNO;
                TempData["PASSPORTNO"] = obj.PASSPORTNO;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index1(Application obj, string Create, string Prev)
        {
            if (!string.IsNullOrEmpty(Prev))
            {
                TempData["Page1"] = Session["holdTempdata1"];
                if (TempData["Page1"] == null)
                {

                }
                TempData["Page2"] = obj;
                return Redirect("/ApplicationMas/Index");
            }
            if (!string.IsNullOrEmpty(Create))
            {
                Application obj1 = Session["holdTempdata1"] as Application;

                List<Application> app1 = new List<Application>();
                List<Application> app2 = new List<Application>();

                //app1.Add(new Application(0, obj1.Name, obj1.Address, obj1.Mobileno, obj1.Birthdate, obj1.EmailID, 0, 0, 0, 0, 0));

            }
            return View();
        }
    }
}
