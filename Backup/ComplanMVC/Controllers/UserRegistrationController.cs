using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class UserRegistrationController : Controller
    {
        //
        // GET: /UserRegistration/
        public ActionResult Index()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            li.Add(new SelectListItem { Text = "West Bengal", Value = "7" });
            li.Add(new SelectListItem { Text = "Karnataka", Value = "8" });
            li.Add(new SelectListItem { Text = "Bihar", Value = "9" });
            li.Add(new SelectListItem { Text = "Andhrapradesh", Value = "10" });
            ViewBag.Statelists = li;
            return View();
        }

        [HttpPost]
        public ActionResult Getcitys(string stateId)
        {
            int statId;
            List<Login> ilist = new List<Login>();
            if (!string.IsNullOrEmpty(stateId))
            {
                statId = Convert.ToInt32(stateId);
                DataAccessLayer.DBdata obj = new DataAccessLayer.DBdata();
                DataTable dtTotal = new DataTable();
                dtTotal = obj.GetCityByState(Convert.ToInt32(stateId));
                ilist = dtTotal.AsEnumerable().Select(data1 => new Login()
                {
                    cityid = Convert.ToInt32(data1["CityId"]),
                    cityname = Convert.ToString(data1["CityName"])
                }).ToList();
            }
            return Json(ilist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Index")]
        public ActionResult Register(Login ob, string Register)
        {
            if (!string.IsNullOrEmpty(Register))
            {
                if (ModelState.IsValid)
                {
                    string UserId = ob.UserId;
                    string Password = ob.Password;
                    string UserName = ob.UserName;
                    string stateid = Convert.ToString(ob.stateid);
                    string cityid = Convert.ToString(ob.cityid);
                }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));
                }
            }
            return RedirectToAction("Index"); // call pageload
        }

    }
}
