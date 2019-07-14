using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplanMVC.Models
{
    public class CommonViewClass
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public string DeptDescription { get; set; }
        public string DeptHead { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public Nullable<decimal> DeptHeadSalary { get; set; }
        public int CountryId { get; set; }

        public int VendorId { get; set; }
        public string VendorContactName { get; set; }
        public Nullable<int> VendorContactNo { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Citycode { get; set; }
    }
}