using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UtubeApplication.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please input a Username")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string Gender { get; set; }
       
              
       
    }
}