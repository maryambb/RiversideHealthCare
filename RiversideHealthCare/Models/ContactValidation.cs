using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(ContactValidation))]
    //name of database table
    public partial class contact_list
    {

    }

    [Bind(Exclude = "Id")]
    public class ContactValidation
    {

        // These are series of inputs in my form with validation. 
        //departments
        [DisplayName("Department")]
        [Required(ErrorMessage = "Please enter department")]
        public string department { get; set; }

        //details
        [DisplayName("Detail")]
        [Required(ErrorMessage = "Please enter details")]
        public string detail { get; set; }

        //departments
        [DisplayName("Location")]
        [Required(ErrorMessage = "Please enter location")]
        public string location { get; set; }

        //details
        [DisplayName("Phone")]
        [Required(ErrorMessage = "Please enter phone")]
        public string phone { get; set; }

        //departments
        [DisplayName("Fax")]
        [Required(ErrorMessage = "Please enter fax no.")]
        public string fax { get; set; }

        //details
        [DisplayName("Head")]
        [Required(ErrorMessage = "Please enter head")]
        public string head { get; set; }

    }
}

