using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Newsletter.Models
{
    public class SubscribersViewModel
    {
        public System.Guid Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Source { get; set; }
        public string Reason { get; set; }
    }
}