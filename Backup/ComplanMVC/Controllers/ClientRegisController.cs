using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ComplanMVC.Controllers
{
    public class ClientRegisController : Controller
    {
        //
        // GET: /ClientRegis/
        public ActionResult Index()
        {
            ClientRegisClass ob = new ClientRegisClass();
            if (ModelState.IsValid)
            {
                ViewBag.ClientName = ob.ClientName;
                ViewBag.ClientCode = ob.ClientCode;
                ViewBag.Clienttype = ob.Clienttype;

                ob.Fields = new List<ClientRegisClass>();
                ob.Fields.Add(new ClientRegisClass { typeid = 1, typename = "Payroll" });
                ob.Fields.Add(new ClientRegisClass { typeid = 0, typename = "Non Payroll" });
                ob.SelectedFieldId = 1; //selecting the default value
                ViewData["statusforinsert"] = TempData["insertalert"];
                if (TempData["AllCli"] != null)
                {
                    ViewBag.ViewClient = TempData["AllCli"];
                }
                else
                {
                    ViewBag.ViewClient = GetClients();
                }
                return View(ob);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)] 
        [ActionName("Index")] // it is used for prevent same view name in one controler
        public ActionResult ClientMigrationSave(ClientRegisClass CM, FormCollection form, string Save, string View)
        {
            string result = "";
            if (!string.IsNullOrEmpty(Save))
            {
                if (ModelState.IsValid) //checking model is valid or not
                {
                    string ClientName = CM.ClientName;
                    string ClientCode = CM.ClientCode;
                    string strtypeid = Convert.ToString(form["Fields"]);
                    DataAccessLayer.DBdata ob = new DataAccessLayer.DBdata();
                    result = ob.InsertClientMigration(CM, strtypeid); // passing Value to DBClass from model
                    TempData["insertalert"] = result;
                    if (result != "-1")
                    {
                        TempData["AllCli"] = GetClients();
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    ModelState.AddModelError("", "Error in saving data");
                    //return View();
                }
            }
            else if (!string.IsNullOrEmpty(View))
            {
                TempData["AllCli"] = GetClients();
            }
            return RedirectToAction("Index"); // call page load action name
        }
        protected List<ClientRegisClass> GetClients()
        {
            DataAccessLayer.DBdata obj = new DataAccessLayer.DBdata();
            DataTable dtallclients = obj.SelectAlClientlData();
            List<ClientRegisClass> ilist = new List<ClientRegisClass>();
            ilist = dtallclients.AsEnumerable().Select(data => new ClientRegisClass()
            {
                ClientId = Convert.ToInt32(data["ClientId"]),
                ClientName = Convert.ToString(data["ClientName"]),
                ClientCode = Convert.ToString(data["ClientCode"]),
                Clienttype = Convert.ToString(data["Clienttype"]),
                clienttypeid = Convert.ToInt32(data["clienttypeid"])
            }).ToList();
            return ilist;
        }
        public ActionResult Delete(int id)
        {
            return RedirectToAction("ClientMigration"); // call page load action name
        }
    }
}
