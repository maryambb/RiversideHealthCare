using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Linq;


namespace RiversideHealthCare.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("riversideConnectionString")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    //public class RegisterModel
    //{
    //    [Required]
    //    [Display(Name = "User name")]
    //    public string UserName { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //}



    //-------------------------------------------------------------//
    // This model is customized. It is only used to create patients//
    //-------------------------------------------------------------//
    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /* Addd extra fields to match with hospital policy */

        [Required]
        [Display(Name = "Health card number")]
        public string health_card { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Date of birth (yyyy/mm/dd)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime bith_date { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email.")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number.")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$", ErrorMessage = "Please enter a valid postal code.")]
        public string postal_code { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string province { get; set; }
    }
    //--------------------------------------------------------------------------------//
    // This model is the same as RegisterModel, but used of updating exsiting patients//
    //--------------------------------------------------------------------------------//
    public class UpdatePatientModel
    {
        [Required]
        [Display(Name = "Health card number")]
        public string health_card { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Date of birth (yyyy/mm/dd)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime birth_date { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email.")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number.")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$", ErrorMessage = "Please enter a valid postal code.")]
        public string postal_code { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string province { get; set; }

    }
    //----------------------------------------------------------------------------//
    // This model is the same as RegisterModel, but used for creating new doctors //
    //----------------------------------------------------------------------------//
    public class CreateDoctorModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /* Addd extra fields to match with hospital policy */

        [Required]
        [Display(Name = "Department")]
        public string department_name { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email.")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number.")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string specialty { get; set; }

        [Required]
        [Display(Name = "Biography")]
        public string bio { get; set; }

     
        [Display(Name = "Photo")]
        [Required(ErrorMessage = "Please upload your photo")]
        HttpPostedFileBase photo_path { get; set; }

    }
    //----------------------------------------------------------------------------------//
    // This model is the same as RegisterModel, but used for updating existring doctors //
    //----------------------------------------------------------------------------------//
    public class UpdateDoctorModel
    {
        [Required]
        [Display(Name = "Department")]
        public string department_name { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email.")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number.")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string specialty { get; set; }

        [Required]
        [Display(Name = "Biography")]
        public string bio { get; set; }

        [Display(Name = "Photo URL")]
        public string photo_path { get; set; }

    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
