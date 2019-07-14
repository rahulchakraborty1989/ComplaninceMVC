using ComplanMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ComplanMVC.WebAPI
{
    public class CollegeController : ApiController
    {
        // GET api/college
        public List<College> Get()
        {
            List<College> teachers = new List<College>();
            teachers.Add(new College { CId = 1, College_Name = "TIG", College_Address = "SALT LAKE" });
            teachers.Add(new College { CId = 2, College_Name = "IIT", College_Address = "KHARAGPUR" });
            teachers.Add(new College { CId = 3, College_Name = "IIM", College_Address = "KOLKATA" });
            teachers.Add(new College { CId = 4, College_Name = "ISBWM", College_Address = "NAIHATI" });
            return teachers;
        }

        // GET api/college/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/college
        public void Post([FromBody]string value)
        {
        }

        // PUT api/college/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/college/5
        public void Delete(int id)
        {
        }
    }
}
