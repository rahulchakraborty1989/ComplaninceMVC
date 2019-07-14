using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplanMVC.Models
{
    public class ClientRegisClass
    {
        [Key]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "ClientName is Required")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "ClientCode is Required")]
        public string ClientCode { get; set; }
        [Required(ErrorMessage = "Clienttype is Required")]
        public string Clienttype { get; set; }
        public int clienttypeid { get; set; }

        public List<ClientRegisClass> Fields { get; set; }
        public int SelectedFieldId { get; set; }

        [Required(ErrorMessage = "typeid")]
        public int typeid { get; set; }
        [Required(ErrorMessage = "typename")]
        public string typename { get; set; }

        public List<ClientRegisClass> list_allclients { get; set; }

    }
}