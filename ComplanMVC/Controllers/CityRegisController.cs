using ComplanMVC.EntityFrameworkModelClass;
using ComplanMVC.Models;
using ComplanMVC.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class CityRegisController : Controller
    {
        //
        // GET: /CityRegis/
        private DBComplianceEntities db = new DBComplianceEntities();
        private ICity Iob;
        public CityRegisController()
        {
            this.Iob = new CityRepository(new DBComplianceEntities());
        }
        
        public ActionResult Index()
        {
            ViewBag.Statelists = SelectListItem();
            ViewBag.Citys = Iob.GetCity().ToList();
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
        public ActionResult savedata(CityMaster obj, string Save, string Viewshow)
        {
            #region #########SHOW###############
            if (!string.IsNullOrEmpty(Viewshow))
            {
                ViewBag.Statelists = SelectListItem();
                //ViewBag.Citys = Iob.GetCity_ByStateID(obj.StateId).ToList();
                return View();
            }
            #endregion

            return RedirectToAction("Index"); // call pageload
        }
    }
}
