
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessEvents.Models
{
    public class EventLocation
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter city latitude")]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Please enter city longitude ")]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "Please enter description ")]
        [Display(Name = "Event Description")]
        public string Description { get; set; }

    }
}