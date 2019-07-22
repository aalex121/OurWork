using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

using System;

namespace OurWork.Models
{
    [Table("JobOffers")]
    public class JobOffer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string OfferName { get; set; }

        public string OfferDescription { get; set; }

        public DateTime PublishDate { get; set; }

        public int UserId { get; set; }

        public UserProfile User { get; set; }

        public int ProfessionId { get; set; }

        public Profession Profession { get; set; }        

        //public ApplicantCharacteristics Reqirements { get; set; }        
    }
}