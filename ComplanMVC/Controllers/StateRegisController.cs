using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class StateRegisController : Controller
    {
        //
        // GET: /StateRegis/
        public ActionResult Index()
        {
            return View();
        }
        
    }
}
