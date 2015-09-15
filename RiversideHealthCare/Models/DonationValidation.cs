using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RiversideHealthCare.Models
{

    [MetadataType(typeof(DonationValidation))]
    public partial class Donation
    {

    }

    [Bind(Exclude = "id")]
    public class DonationValidation
    {
        // These are series of inputs in my form with validation. 
        //Some of them like First Name just required to display and some of them like Email has the regular expression.

        //First name
        [DisplayName("First Name:")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string first_name { get; set; }

        //Last name
        [DisplayName("Last Name:")]
        [Required(ErrorMessage = "Please enter you last name")]
        public string last_name { get; set; }

        //Country
        [DisplayName("Country:")]
        [Required(ErrorMessage = "Please select your country")]
        public string country { get; set; }

        //Province
        [DisplayName("Province:")]
        [Required(ErrorMessage = "Please Enter your province")]
        public string province { get; set; }

        //City
        [DisplayName("City:")]
        [Required(ErrorMessage = "Please enter your city")]
        public string city { get; set; }

        //Postal Code
        [DisplayName("Postal Code (e.g. L4J8Z3):")]
        [Required(ErrorMessage = "Please enter your Postal Code")]
        public string postal_code { get; set; }

        //Email
        [DisplayName("Email (donor@example.com):")]
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string email { get; set; }

        //Phone
        [DisplayName("Phone Number (e.g. 647-772-4321):")]
        [Required(ErrorMessage = "Please enter your phone")]
        [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$|^[2-9]\\d{2}\\d{3}\\d{4}$", ErrorMessage = "Please enter a valid phone number")]
        public string phone { get; set; }

        //Designation
        [DisplayName("Designation:")]
        [Required(ErrorMessage = "Please Enter your designation")]
        public int branch_id { get; set; }

        //Date 
        [DisplayName("Date (e.g. yyyy-MM-dd):")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Please enter the date")]
        public DateTime date { get; set; }

        //Amount
        [DisplayName("Amount:")]
        [Required(ErrorMessage = "Please Enter your amount")]
        public decimal amount { get; set; }
    }
}