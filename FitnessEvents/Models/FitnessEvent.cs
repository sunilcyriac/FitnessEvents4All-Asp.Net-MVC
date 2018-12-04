namespace FitnessEvents.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FitnessEvent
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Event Details")]
        public string EventDetails { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public string EventType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Posted Date")]
        public DateTime PostedDate { get; set; }

        [Required]
        [Display(Name = "Image Upload")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Organizer Name")]
        [StringLength(128)]
        public string ApplicationUserId { get; set; }
    }
}
