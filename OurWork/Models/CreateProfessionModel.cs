using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurWork.Models
{
    public class CreateProfessionModel
    {
        public Profession ProfessionData { get; set; }

        public int SkillId { get; set; }

        public int SkillLevelId { get; set; }
    }
}