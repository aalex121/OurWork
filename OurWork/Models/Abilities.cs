using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace OurWork.Models
{
    [Table("Abilities")]
    public class Abilities
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SkillId { get; set; }
        
        public Skills Skill { get; set; }

        public int SkillLevelId { get; set; }
        
        public SkillLevels SkillLevel { get; set; }

        public List<AbilitySets> AbilitySets { get; set; }
    }
}