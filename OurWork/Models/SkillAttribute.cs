using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace OurWork.Models
{
    [Table("SkillAttributes")]
    public class SkillAttribute
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SkillId { get; set; }

        public string Name { get; set; }

        public List<Skill> Skills { get; set; }
    }
}