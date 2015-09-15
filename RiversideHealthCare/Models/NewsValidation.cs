using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
//NEEDED FOR MAIL
using System.IO;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(NewsValidation))]
    public partial class news
    {

    }
    [Bind(Exclude = "id")]
    public class NewsValidation
    {

        [DisplayName("Headline: ")]
        [Required(ErrorMessage = "Please enter a headline")]
        public string news_name { get; set; }

        //FORMATING OF DATETIME DATA TYPE
        [DisplayName("Date Published: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? news_date { get; set; } 

        [DisplayName("Image: ")]
        [Required(ErrorMessage = "Please enter image URL")]
        HttpPostedFileBase news_img { get; set; }

        [DisplayName("News Description: ")]
        [Required(ErrorMessage = "Please enter a description")]
        public string news_desc { get; set; }

    }

}