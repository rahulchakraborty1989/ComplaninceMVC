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
    public class DeptMasterController : Controller
    {
        //
        // GET: /DeptMaster/

        private DBComplianceEntities db = new DBComplianceEntities();
        public ActionResult Index()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            /* Static list create
            //li.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            //li.Add(new SelectListItem { Text = "West Bengal", Value = "7" });
            //li.Add(new SelectListItem { Text = "Karnataka", Value = "8" });
            //li.Add(new SelectListItem { Text = "Bihar", Value = "9" });
            //li.Add(new SelectListItem { Text = "Andhrapradesh", Value = "10" });
            //ViewBag.Statelists = li;
             * */

            ViewBag.savemsg = TempData["alert"];

            /* Dynamic list create
             * */
            ViewBag.Statelists = SelectListItem();

            // ViewBag.DeptName = db.DepartmentMasters.ToList();
            ViewBag.DeptName = (from s in db.DepartmentMasters
                                join sa in db.StateMasters on s.DeptState equals sa.StateId
                                select new CommonViewClass
                                {
                                    DeptId = s.DeptId,
                                    DeptName = s.DeptName,
                                    DeptDescription = s.DeptDescription,
                                    DeptHead = s.DeptHead,
                                    DeptHeadSalary = (decimal)s.DeptHeadSalary,
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
            // dtstate = ToDataTable(db.StateMasters.ToList());
            dtstate = ToDataTable(
                   (from s in db.StateMasters
                    orderby s.StateName
                    select s).ToList()
                );
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
        public ActionResult savedata(DepartmentMaster obj, string Save, string Viewshow, string Update)
        {
            #region ##############SAVE##############
            if (!string.IsNullOrEmpty(Save))
            {
                var DepartmentName = obj.DeptName;
                var checkduplicacy = (from x in db.DepartmentMasters
                                      where x.DeptName == DepartmentName && x.DeptState == obj.DeptState
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
                        db.DepartmentMasters.Add(obj);
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

            #region #########SHOW###############
            if (!string.IsNullOrEmpty(Viewshow))
            {
                ViewBag.Statelists = SelectListItem();

                ViewBag.DeptName = (from s in db.DepartmentMasters
                                    join sa in db.StateMasters on s.DeptState equals sa.StateId
                                    where s.DeptState == obj.DeptState
                                    select new CommonViewClass
                                    {
                                        DeptId = s.DeptId,
                                        DeptName = s.DeptName,
                                        DeptDescription = s.DeptDescription,
                                        DeptHead = s.DeptHead,
                                        DeptHeadSalary = (decimal)s.DeptHeadSalary,
                                        StateName = sa.StateName,
                                        StateId = sa.StateId
                                    }).ToList();
                return View();
            }
            #endregion

            #region #########SHOW###############
            if (!string.IsNullOrEmpty(Update))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["alert"] = "2";
                    return RedirectToAction("Index");
                }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));
                }
            }
            #endregion

            return RedirectToAction("Index"); // call pageload
        }

        public ActionResult Delete(int id)
        {
            DepartmentMaster obj = db.DepartmentMasters.Find(id);
            db.DepartmentMasters.Remove(obj);
            db.SaveChanges();
            TempData["alert"] = "3";
            return RedirectToAction("Index");
        }
        public ActionResult StateDetails(int StateId)
        {
            ViewBag.StateDetails = (from s in db.CityMasters where s.StateId == StateId select s);
            return PartialView("StateDetailsPartialView");
        }
    }
}
