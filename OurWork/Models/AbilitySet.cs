using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace OurWork.Models
{
    [Table("AbilitySets")]
    public class AbilitySet
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AbilitiesId { get; set; }
        
        public Ability Abilities { get; set; }

        public int ApplianceId { get; set; }        

        //public Professions Profession { get; set; }
    }
}