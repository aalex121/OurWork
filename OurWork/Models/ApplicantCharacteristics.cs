using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace OurWork.Models
{
    [Table("ApplicantCharacteristics")]
    public class ApplicantCharacteristics
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Age { get; set; }

        public int EducationId { get; set; }

        public EducationTypes Education { get; set; }
         
        public int WorkExpirience { get; set; }

        public int OfferId { get; set; }

        //[Required]
        //public JobOffers Offer { get; set; }        

        public int ApplicantId { get; set; }

        //[Required]
        //public UserData Applicant { get; set; }
    }
}