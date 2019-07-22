using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurWork.Models
{
    public class CreateApplianceModel
    {
        public CreateApplianceModel()
        {
            ApplianceAbilities = new List<Ability>();
        }

        public JobAppliance ApplianceData { get; set; }

        public List<Ability> ApplianceAbilities { get; set; }
    }
}