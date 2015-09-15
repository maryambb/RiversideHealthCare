using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(VolunteerApplicationValidation))]
    public partial class volunteer_application_form
    {

    }

    [Bind(Exclude = "id")]
    public partial class VolunteerApplicationValidation
    {
        // These are series of inputs in my form with validation. 
        //Some of them like First Name just required to display and some of them like Email has the regular expression.
        //First Name
        [DisplayName("First Name:")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string fname { get; set; }

        //Last Name
        [DisplayName("Last Name:")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string lname { get; set; }

        //Address
        [DisplayName("Address:")]
        [Required(ErrorMessage = "Please enter the address")]
        public string address { get; set; }

        //City
        [DisplayName("City:")]
        [Required(ErrorMessage = "Please enter your city")]
        public string city { get; set; }

        //Postal code
        [DisplayName("Postal Code:")]
        [Required(ErrorMessage = "Please enter your Postal Code")]
        public string postal_code { get; set; }

        //Country
        [DisplayName("Country:")]
        [Required(ErrorMessage = "Please enter country name")]
        public string country { get; set; }

        //Phone
        [DisplayName("Phone:")]
        [Required(ErrorMessage = "Please enter your phone number")]
        // [Range(0, 10, ErrorMessage = "Enter valid range of numbers")]
        public string phone { get; set; }

        //Email
        [DisplayName("Email:")]
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string email { get; set; }

        //Gender
        [DisplayName("Gender:")]
        [Required(ErrorMessage = "Please enter gender")]
        public string gender { get; set; }


        //College
        [DisplayName("College:")]
        [Required(ErrorMessage = "Please enter college name")]
        public string college { get; set; }

        //Year
        [DisplayName("Year:")]
        [Required(ErrorMessage = "Please enter year")]
        public string year { get; set; }

        //Language
        [DisplayName("Language:")]
        [Required(ErrorMessage = "Please enter language")]
        public string language { get; set; }

        //Volunteer Experience
        [DisplayName("Volunter Experience(if any):")]
        public string vol_exp { get; set; }

        //Availability
        [DisplayName("Availability:")]
        [Required]
        public string availability { get; set; }

    }
}