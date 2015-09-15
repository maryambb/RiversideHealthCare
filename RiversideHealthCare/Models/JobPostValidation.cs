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
    [MetadataType(typeof(JobPostValidation))]
    public partial class JobPost
    {

    }

    [Bind(Exclude = "id")]
    public class JobPostValidation
    {
        // These are series of inputs in my form with validation. 
        //Some of them like First Name just required to display and some of them like Email has the regular expression.
        //Job Category
        [DisplayName("Job Category:")]
        [Required(ErrorMessage = "Please Choose the Job Category")]
        public int JobCategory_id { get; set; }

        //Title
        [DisplayName("Title:")]
        [Required(ErrorMessage = "Please enter the job title")]
        public string title { get; set; }

        //Description
        [DisplayName("Description:")]
        [Required(ErrorMessage = "Please enter the job description")]
        [AllowHtml]
        public string description { get; set; }

        //Date 
        [DisplayName("Date (e.g. yyyy-MM-dd):")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Please enter the date")]
        public DateTime date { get; set; }

        //Email
        [DisplayName("Email (employer@example.com):")]
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string email { get; set; }

        //Status
        [DisplayName("Status:")]
        [Required(ErrorMessage = "Please enter the job status")]
        public string status { get; set; }

    }
}