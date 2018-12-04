namespace FitnessEvents.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FitnessEvents_Models : DbContext
    {
        public FitnessEvents_Models()
            : base("name=FitnessEvents_Models")
        {
        }

        public virtual DbSet<FitnessEvent> FitnessEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<FitnessEvents.Models.EventRegistration> EventRegistrations { get; set; }
    }
}
