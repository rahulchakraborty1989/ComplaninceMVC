using ComplanMVC.EntityFrameworkModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class EntityClientRegisController : Controller
    {
         //
        // GET: /EntityClientRegis/

        private DBComplianceEntities ob = new DBComplianceEntities();

        public ActionResult Index()
        {
            ViewBag.CLientMasterlist = ob.ClientMasters.ToList();
            return View();
        }


    }
}
