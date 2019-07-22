using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurWork.SearchLogic
{
    public class SkillLevelComparer
    {
        private readonly DataContext _context;
        private readonly int _spreadValue;

        public SkillLevelComparer(int spreadValue)
        {
            _context = new DataContext();
            _spreadValue = spreadValue;
        }

        public bool SkillLevelFits(int currentSkillLevelId, int desiredSkillLevelId)
        {
            int currentSkillValue = _context.SkillLevels.Find(currentSkillLevelId).Value;
            int desiredSkillValue = _context.SkillLevels.Find(desiredSkillLevelId).Value;

            bool result = (currentSkillValue >= desiredSkillValue - _spreadValue) ||
                    (currentSkillValue <= desiredSkillValue + _spreadValue);

            return result;
        }


    }
}