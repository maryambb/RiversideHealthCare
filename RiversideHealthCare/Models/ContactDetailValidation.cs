using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(ContactDetailValidation))]
    //name of database table
    public partial class contact_detail
    {

    }

    [Bind(Exclude = "Id")]
    public class ContactDetailValidation
    {

        // These are series of inputs in my form with validation. 

        //name
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter name")]
        public string name { get; set; }

        //phone
        [DisplayName("Phone")]
        [Required(ErrorMessage = "Please enter phone number")]
        public string phone { get; set; }

    }
}

