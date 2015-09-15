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
    [MetadataType(typeof(ImagesliderValidation))]
    //name of database table
    public partial class Imageslider
    {

    }

    [Bind(Exclude = "ImageId")]
    public partial class ImagesliderValidation
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [DisplayName("Image Upload")]
        [Required(ErrorMessage = "Please enter an image")]
        HttpPostedFileBase Image { get; set; }

        [DisplayName("Comment")]
        [Required(ErrorMessage = "Please enter comment")]
        public string Comment { get; set; }

        [DisplayName("Link")]
        [Required(ErrorMessage = "Please enter link for this image")]
        public string Link { get; set; }

    }
}