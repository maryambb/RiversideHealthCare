using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(OrderValidation))]
    //name of database table
    public partial class Order
    {

    }

    public partial class OrderValidation
    {
        //properties for user information to be sent as an order detail
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string firstName { get; set; }

        [DisplayName("lastName")]
        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string lastName { get; set; }

        [DisplayName("Street")]
        [Required(ErrorMessage = "Please enter your street")]
        public string street { get; set; }

        [DisplayName("Postal")]
        [Required(ErrorMessage = "Please enter your postal code")]
        public string postal { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "Please enter your city")]
        public string city { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Please enter your country")]
        public string country { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string email { get; set; }

        //Date 
        [DisplayName("Date (e.g. yyyy-MM-dd):")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Please enter the date")]
        public DateTime date { get; set; }

    }
}