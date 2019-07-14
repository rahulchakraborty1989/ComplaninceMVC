using ComplanMVC.EntityFrameworkModelClass;
using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class VendorContactInfoController : Controller
    {
        //
        // GET: /VendorContactInfo/
        private DBComplianceEntities db = new DBComplianceEntities();

        public ActionResult Index()
        {
            //return RedirectToAction("Index", "CityRegis"); // call one controller action from another controller mvc

            ViewBag.savemsg = TempData["alert"];

            List<SelectListItem> li = new List<SelectListItem>();
            ViewBag.Statelists = SelectListItem();

            List<CommonViewClass> lst = new List<CommonViewClass>();

            ViewBag.Vendordisplay = (from s in db.VendorContactInfoes
                                join sa in db.StateMasters on s.StateID equals sa.StateId
                                select new CommonViewClass
                                {
                                    VendorId=s.ID,
                                    VendorContactName = s.VendorContactName,
                                    VendorContactNo = s.VendorContactNo,
                                    StateName = sa.StateName,
                                    StateId = sa.StateId
                                }).ToList();

            return View();
        }
        public List<SelectListItem> SelectListItem()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Select--", Value = "-1" });
            List<CommonViewClass> lst = new List<CommonViewClass>();
            DataTable dtstate = new DataTable();
            dtstate = ToDataTable(db.StateMasters.ToList());
            lst = dtstate.AsEnumerable().Select(data => new CommonViewClass()
            {
                StateId = Convert.ToInt32(data["StateId"]),
                StateName = Convert.ToString(data["StateName"])
            }).ToList();

            for (var i = 0; i < lst.Count; i++)
            {
                li.Add(new SelectListItem { Text = lst[i].StateName, Value = Convert.ToString(lst[i].StateId) });
            }
            return li;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Index")]
        public ActionResult savedata(VendorContactInfo obj, string Save, string Viewshow, int[] chkvendors)
        {
            #region ##############SAVE##############
            if (!string.IsNullOrEmpty(Save))
            {
                var VendorContactName = obj.VendorContactName;
                var checkduplicacy = (from x in db.VendorContactInfoes
                                      where x.VendorContactName == VendorContactName && x.VendorContactNo == obj.VendorContactNo
                                      select x
                    ).ToList();

                if (ModelState.IsValid)
                {
                    if (checkduplicacy.Count > 0)
                    {
                        ViewBag.Statelists = SelectListItem();
                        TempData["alert"] = "0";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        db.VendorContactInfoes.Add(obj);
                        db.SaveChanges();
                        TempData["alert"] = "1";
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));
                }
            }
            #endregion

            #region #########BulkSave###############
            if (!string.IsNullOrEmpty(Viewshow))
            {
                
            }
            #endregion
            return RedirectToAction("Index"); // call pageload
        }

        public ActionResult Delete(int id)
        {
            VendorContactInfo obj = db.VendorContactInfoes.Find(id);
            db.VendorContactInfoes.Remove(obj);
            db.SaveChanges();
            TempData["alert"] = "3";
            return RedirectToAction("Index");
        }
    }
}
