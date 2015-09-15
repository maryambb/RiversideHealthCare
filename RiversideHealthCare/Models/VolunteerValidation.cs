using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(VolunteerValidation))]
    //name of database table
    public partial class Volunteer
    {

    }

    [Bind(Exclude = "Id")]
    public class VolunteerValidation
    {

        // These are series of inputs in my form with validation. 

        //Volunteer Position
        [DisplayName("Volunteer Position:")]
        [Required(ErrorMessage = "Please enter position")]
        public string position { get; set; }

        //Locations
        [DisplayName("List of Location:")]
        [Required(ErrorMessage = "Please enter location")]
        public string location { get; set; }

        //Volunteer Requirements
        [DisplayName("Volunteer Requirements:")]
        [Required(ErrorMessage = "Please enter requirements")]
        public string requirement { get; set; }
    }
}

