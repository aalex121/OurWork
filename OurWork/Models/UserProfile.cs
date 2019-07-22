using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

using System;
using System.Collections.Generic;

namespace OurWork.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public UserRole Role { get; set; }
        //public UserData UserData { get; set; }
        public List<JobAppliance> JobAppliances { get; set; }
        public List<JobOffer> JobOffers { get; set; }
    }
}