using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.WebUI.Models
{
    public class LogInModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}