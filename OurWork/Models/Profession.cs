using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace OurWork.Models
{
    [Table("Professions")]
    public class Profession
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int AbilitySetId { get; set; }

        //[Required]
        //public AbilitySets AbilitySet { get; set; }

        public List<JobOffer> JobOffers { get; set; }
    }
}