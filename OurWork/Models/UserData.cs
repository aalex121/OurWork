using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

using System;

namespace OurWork.Models
{
    [Table("UserData")]
    public class UserData
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string CompanyName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int UserId { get; set; }

        //[Required]
        //public UserProfile User { get; set; }
    }
}