using OurWork.Models;
using OurWork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurWork.SearchLogic
{
    public class ApplicantFinder
    {
        private readonly DataContext _context;
        private int _spreadValue;

        public ApplicantFinder(int spreadValue)
        {
            _context = new DataContext();
            _spreadValue = spreadValue;
        }

        public List<ApplicantSearchResults> SearchApplicants(int offerId)
        {
            JobOffer currentOffer = _context.JobOffers.Find(offerId);
            Profession currentProfession = _context.Professions.Where(p => p.Id == currentOffer.ProfessionId).FirstOrDefault();
            SkillLevelComparer skillComparer = new SkillLevelComparer(_spreadValue);

            var desiredSkills = _context.AblitySets.Join(_context.Abilities,
                                    aset => aset.AbilitiesId,
                                    abil => abil.Id,
                                    (aset, abil) => new
                                    {
                                        SkillId = abil.SkillId,
                                        LevelId = abil.SkillLevelId,
                                        SetId = aset.Id
                                    }).
                                    Where(r => r.SetId == currentProfession.AbilitySetId).
                                    Select(r => new { SkillId = r.SkillId, SkillLevelId = r.LevelId }).
                                    FirstOrDefault();

            var userAbilitySets = _context.AblitySets.Where(aset => aset.ApplianceId != null && aset.ApplianceId > 0);

            var matchingApplianceIds = userAbilitySets.Join(_context.Abilities,
                                    aset => aset.AbilitiesId,
                                    abil => abil.Id,
                                    (aset, abil) => new
                                    {
                                        SkillId = abil.SkillId,
                                        LevelId = abil.SkillLevelId,
                                        ApplianceId = aset.ApplianceId
                                    }).
                                    Where(r => r.SkillId == desiredSkills.SkillId).
                                    ToArray().
                                    Where(r => skillComparer.SkillLevelFits(r.LevelId, desiredSkills.SkillLevelId)).
                                    Select(r => r.ApplianceId);

            var matchingAppliances = _context.JobApplicances.Where(a => matchingApplianceIds.Contains(a.Id));

            List<ApplicantSearchResults> results = matchingAppliances.Join(_context.UserData,
                                            appl => appl.UserId,
                                            udata => udata.UserId,
                                            (appl, udata) => new ApplicantSearchResults
                                            {
                                                Appliance = appl,
                                                Applicant = udata
                                            }
                                            ).ToList();

            return results;
        }
    }
}