using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(erValidation))]
    public partial class ERWaitQueue
    {

    }
    [Bind(Exclude = "id")]
    public partial class erValidation
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string fName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string lName { get; set; }

        [DisplayName("Condition")]
        [Range(1, 3)]
        [Required(ErrorMessage = "Please enter your condition.")]
        public string condition { get; set; }
    }
}