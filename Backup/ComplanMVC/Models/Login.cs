using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplanMVC.Models
{
    public class Login
    {
        [Key]

        [Required(ErrorMessage = "Enter UserName")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }

        public string UserName { get; set; }

        public int Isstatus { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int City { get; set; }

        public List<Login> StatesList { get; set; }
        [Required(ErrorMessage = "stateid")]
        public int stateid { get; set; }
       // [Required(ErrorMessage = "statename")]
        public string statename { get; set; }

        public List<Login> CityList { get; set; }
        [Required(ErrorMessage = "cityid")]
        public int cityid { get; set; }
       // [Required(ErrorMessage = "cityname")]
        public string cityname { get; set; }
    }
}