using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace OurWork.Models
{
    [Table("JobAppliances")]
    public class JobAppliances
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public UserProfile User { get; set; }        
    }
}