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
    [MetadataType(typeof(ApplicantValidation))]
    public partial class JobApplicant
    {

    }

    [Bind(Exclude = "id")]
    public class ApplicantValidation
    {
        // These are series of inputs in my form with validation. 
        //Some of them like First Name just required to display and some of them like Email has the regular expression.

        //Job Post ID
        [DisplayName("Job Post ID:")]
        //[Required(ErrorMessage = "Please Enter your designation")]
        public int JobPost_id { get; set; }

        //First name
        [DisplayName("First Name:")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string first_name { get; set; }

        //Last name
        [DisplayName("Last Name:")]
        [Required(ErrorMessage = "Please enter you last name")]
        public string last_name { get; set; }

        //Email
        [DisplayName("Email (applicant@example.com):")]
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string email { get; set; }

        //Phone
        [DisplayName("Phone Number (e.g. 647-772-4321):")]
        [Required(ErrorMessage = "Please enter your phone")]
        [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$|^[2-9]\\d{2}\\d{3}\\d{4}$", ErrorMessage = "Please enter a valid phone number")]
        public string phone { get; set; }

        //Resume
        [DisplayName("Resume:")]
        [Required(ErrorMessage = "Please upload you resume")]
        //[FileExtensions(Extensions = "txt,doc,docx,pdf", ErrorMessage = "Please upload valid format")]
        HttpPostedFileBase resume_path { get; set; }

        //Cover Letter
        [DisplayName("Cover Letter:")]
        [Required(ErrorMessage = "Please upload you cover letter")]
        HttpPostedFileBase cover_letter_path { get; set; }

        //Status
        [DisplayName("Status:")]
        public string status { get; set; }

        //Description
        [DisplayName("Description:")]
        public decimal description { get; set; }

        //Date 
        [DisplayName("Date of apply:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Please enter the date")]
        public DateTime date { get; set; }



    }
}