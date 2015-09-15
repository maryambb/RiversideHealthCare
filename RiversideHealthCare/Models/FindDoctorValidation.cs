using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RiversideHealthCare.Models
{

        [MetadataType(typeof(FindDoctorValidation))]
        //name of database table
        public partial class findDoctor
        {

        }

        [Bind(Exclude = "Id")]
        public class FindDoctorValidation
        {

            // These are series of inputs in my form with validation. 

            //first Name
            [DisplayName("First Name")]
            [Required(ErrorMessage = "Please enter first name")]
            public string fname { get; set; }

            //last name
            [DisplayName("Last Name")]
            [Required(ErrorMessage = "Please enter last name")]
            public string lname { get; set; }

            //speciality
            [DisplayName("Speciality")]
            [Required(ErrorMessage = "Please enter speciality of the doctor")]
            public string speciality { get; set; }

            //location
            [DisplayName("Location")]
            [Required(ErrorMessage = "Please enter location")]
            public string branch_id { get; set; }

            //gender
            [DisplayName("Gender")]
            [Required]
            public string gender { get; set; }

        }
    }


