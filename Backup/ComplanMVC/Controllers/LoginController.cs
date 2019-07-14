using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login ob)
        {
            if (ModelState.IsValid)
            {
                DataAccessLayer.DBdata obdb = new DataAccessLayer.DBdata();
                string uid = ob.UserId;
                string pass = ob.Password;
                string result = obdb.Logincheck(ob);
                if (result != "")
                {
                    Session["UserName"] = Convert.ToString(result);
                    return RedirectToAction("Index", "ClientRegis");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            }
            return View();
        }
       
        public ActionResult Logout()
        {
            Session.Remove("Username");
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session["UserName"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
