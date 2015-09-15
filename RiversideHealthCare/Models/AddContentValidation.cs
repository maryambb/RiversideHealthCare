using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(AddContentValidation))]
    //name of database table
    public partial class Navigation
    {

    }

    //validation properties
    [Bind(Exclude = "id")]
    public partial class AddContentValidation
    {
        [DisplayName("Title")]
        [Required(ErrorMessage = "Please enter title")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string title { get; set; }

        [DisplayName("Subtitle")]
        [Required(ErrorMessage = "Please enter subtitle")]
        public string subtitle { get; set; }

        [DisplayName("Intro")]
        [Required(ErrorMessage = "Please enter introduction text")]
        public string intro { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter a sentence to describe this page")]
        public string description { get; set; }

        [DisplayName("Content Text")]
        [Required(ErrorMessage = "Please enter some text for your page")]
        [AllowHtml]
        public string content_text { get; set; }

        //allow for image upload
        [Required(ErrorMessage = "Please enter an image for this page")]
        HttpPostedFileBase content_image { get; set; }

    }
}