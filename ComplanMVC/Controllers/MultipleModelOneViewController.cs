using ComplanMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
//using System.Net.Http.HttpClient;
using System.Threading.Tasks;
using System.Configuration;

namespace ComplanMVC.Controllers
{
    public class MultipleModelOneViewController : Controller
    {
        //
        // GET: /MultipleModelOneView/

        public ActionResult Index()
        {
            ViewBag.Message = "MULTIPLE MODEL ONE VIEW";
            return View();
        }

        public PartialViewResult RenderTeacherPartial()
        {
            return PartialView(GetTeachers());
        }

        public PartialViewResult RenderStudentPartial()
        {
            return PartialView(GetStudents());
        }

       /* public PartialViewResult RenderCollegePartial()
        {
            return PartialView(GetColleges());
        }*/

        // string Baseurl = "http://localhost:1662/WebAPI/";

        string apiUrl = ConfigurationManager.AppSettings["baseurl"] + "/WebAPI/Get/";
        HttpClient client;
        public MultipleModelOneViewController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        /*public async Task<ActionResult> GetColleges()
        {
            List<College> emp = new List<College>();
            HttpResponseMessage responseMessage = await client.GetAsync(apiUrl);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                emp = JsonConvert.DeserializeObject<List<College>>(responseData);
            }
            return View(emp);
        }*/


        private List<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            teachers.Add(new Teacher { TeacherId = 1, Code = "TT", Name = "Tejas Trivedi" });
            teachers.Add(new Teacher { TeacherId = 2, Code = "JT", Name = "Jignesh Trivedi" });
            teachers.Add(new Teacher { TeacherId = 3, Code = "RT", Name = "Rakesh Trivedi" });
            return teachers;
        }
        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student { StudentId = 1, Code = "L0001", Name = "Amit Gupta", EnrollmentNo = "201404150001" });
            students.Add(new Student { StudentId = 2, Code = "L0002", Name = "Chetan Gujjar", EnrollmentNo = "201404150002" });
            students.Add(new Student { StudentId = 3, Code = "L0003", Name = "Bhavin Patel", EnrollmentNo = "201404150003" });
            return students;
        }
    }
}
