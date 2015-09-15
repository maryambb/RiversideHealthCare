using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(ProductValidation))]
    //name of database table
    public partial class Product
    {

    }

    [Bind(Exclude = "ProductId")]
    public partial class ProductValidation
    {
        //[DisplayName("Product ID")]
        //public int ProductId { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please enter name of product")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter description")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Please enter unit price")]
        [RegularExpression("^[$]?[0-9]*(\\.)?[0-9]?[0-9]?$", ErrorMessage = "Enter valid price value")]
        public decimal UnitPrice { get; set; }

        //allow for image upload
        [DisplayName("Image Upload")]
        [Required(ErrorMessage = "Please enter image URL")]
        HttpPostedFileBase Image { get; set; }

        [DisplayName("Stock")]
        [Required(ErrorMessage = "Please enter current stock volume")]
        public int Stock { get; set; }
    }
}