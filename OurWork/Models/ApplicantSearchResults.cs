using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurWork.Models
{
    public class ApplicantSearchResults
    {
        public JobAppliances Appliance { get; set; }

        public UserData Applicant { get; set; }
    }
}