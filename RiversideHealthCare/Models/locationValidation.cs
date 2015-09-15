using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(locationValidation))]
    public partial class location
    {

    }
    [Bind(Exclude = "id")]
    public partial class locationValidation
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter the name of location")]
        public string name { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Please enter an address")]
        public string address { get; set; }

        [DisplayName("Postal Code")]
        [RegularExpression("^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$", ErrorMessage = "Please enter a postal code")]
        public string postalCode { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Please enter a country")]
        public string country { get; set; }

        [DisplayName("Phone")]
        [RegularExpression("^0[23489]{1}(\\-)?[^0\\D]{1}\\d{6}$", ErrorMessage = "Please enter the phone number of location")]
        public string phone { get; set; }

    }
}