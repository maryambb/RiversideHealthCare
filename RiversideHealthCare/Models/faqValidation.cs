using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(faqValidation))]
    public partial class faq
    {

    }
    [Bind(Exclude = "id")]
    public partial class faqValidation
    {
        [DisplayName("Faq Question")]
        [Required(ErrorMessage = "Please enter Question")]
        public string question { get; set; }

        [DisplayName("Faq Answer")]
        [Required(ErrorMessage = "Please enter Answer")]
        public string answer { get; set; }
    }
}