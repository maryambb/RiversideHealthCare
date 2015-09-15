using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RiversideHealthCare.Models
{
    [MetadataType(typeof(EventsValidation))]
    public partial class events
    {

    }
    [Bind(Exclude = "id")]
    public class EventsValidation
    {
        //ALL EVENT FIELDS ARE REQUIRED
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter an event name")]
        public string ev_title { get; set; }

        [DisplayName("Event Location")]
        [Required(ErrorMessage = "Please enter a location")]
        public DateTime ev_location { get; set; }

        [DisplayName("Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Please enter a date")]
        public string ev_date { get; set; }

        [DisplayName("Event Description")]
        [Required(ErrorMessage = "Please enter a description")]
        public string ev_desc { get; set; }

    }

}