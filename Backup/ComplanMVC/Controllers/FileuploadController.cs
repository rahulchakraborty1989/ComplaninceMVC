using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Controllers
{
    public class FileuploadController : Controller
    {
        //
        // GET: /Fileupload/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };
                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    }
                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    }
                    else
                    {
                        var fileName = Path.GetFileName(file.FileName);

                        var Originalfilename = fileName;
                        string[] arr = fileName.Split('.');
                        var arr1 = arr[0];
                        var arr2 = arr[1];
                        arr1 += DateTime.Now;
                        arr1 = arr1.Replace("/", "");
                        arr1 = arr1.Replace(":", "");
                        arr1 = arr1.Replace(" ", "_");
                        fileName = arr1 +"."+ arr2;

                        var path = Path.Combine(Server.MapPath("~/UploadFolder"), fileName);
                        file.SaveAs(path);
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";
                    }
                }
            }
            return View();
        }
    }
}
