using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplanMVC.Models
{
    public class Application
    {
        [Key]
        public int APPNO { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Mobileno")]
        public string Mobileno { get; set; }

        [DataType(DataType.Date)] // this line makes a textbox to a calender
        [Required(ErrorMessage = "Enter Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Enter EmailID")]
        public string EmailID { get; set; }
        public int STATEID { get; set; }
        public int CITYID { get; set; }
        public string PANNO { get; set; }
        public string ADHARNO { get; set; }
        public string PASSPORTNO { get; set; }
    }
}