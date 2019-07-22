using OurWork.Models;
using OurWork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurWork.SearchLogic
{
    public class JobFinder
    {
        private readonly DataContext _context;

        public JobFinder()
        {
            _context = new DataContext();
        }

        public List<JobOffer> SearchJobs(UserProfile currentUser)
        {
            var applianceIds = _context.JobApplicances.Where(a => a.UserId == currentUser.UserId).Select(a => a.Id);

            var absets = _context.AblitySets.Where(a => applianceIds.Contains(a.ApplianceId));

            var applicantAbilities = absets.Join(_context.Abilities,
                                            abset => abset.AbilitiesId,
                                            abil => abil.Id,
                                            (abset, abil) => abil);            

            var nonUserSkillsetsIds = _context.AblitySets.Join(_context.Abilities,
                                            aset => aset.AbilitiesId,
                                            abil => abil.Id,
                                            (aset, abil) => new
                                            {
                                                setId = aset.Id,
                                                skillId = abil.SkillId,
                                                sklvl = abil.SkillLevelId,
                                                applId = aset.ApplianceId
                                            }).
                                            Where(r => r.applId == 0 || r.applId == null).
                                            Select(r => new { SkillId = r.skillId, SkillLevelId = r.sklvl, SkillSetId = r.setId });

            var matchingSkillSets = applicantAbilities.Join(nonUserSkillsetsIds,
                                            a => a.SkillId,
                                            s => s.SkillId,
                                            (a, s) => new
                                            {
                                                setId = s.SkillSetId,
                                                OwnerSkillLvlId = a.SkillLevelId,
                                                DesiredSkillLvlId = s.SkillLevelId
                                            }).
                                            Where(r => r.OwnerSkillLvlId == r.DesiredSkillLvlId).
                                            Select(r => r.setId);

            List<JobOffer> matchingJobOffers = _context.JobOffers.Join(_context.Professions,
                                            offer => offer.ProfessionId,
                                            prof => prof.Id,
                                            (offer, prof) => new
                                            {
                                                Offer = offer,
                                                AbSetId = prof.AbilitySetId
                                            }).
                                            Where(r => matchingSkillSets.Contains(r.AbSetId)).
                                            Select(r => r.Offer).
                                            ToList();

            return matchingJobOffers;
        }
    }
}