using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(FeedbackValidation))]
    public partial class feedback
    {

    }
    [Bind(Exclude = "id")]
    public class FeedbackValidation
    {
        //ALL FIELDS REQUIRED
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string fb_fname { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string fb_lname { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your email")]
        //REGEX TO VALIDATE EMAIL
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string fb_email { get; set; }

        [DisplayName("Feedback")]
        [Required(ErrorMessage = "Please enter your feedback")]
        public string fb_comment { get; set; }

    }
}