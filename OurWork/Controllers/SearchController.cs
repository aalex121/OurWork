using OurWork.Enums;
using OurWork.Models;
using OurWork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;



namespace OurWork.Controllers
{
    public class SearchController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly UserDataRepository _userDataRepository;
        private readonly ApplicantCharacteristicsRepository _appCharaRepository;

        public SearchController()
        {
            _userRepository = new UserRepository();
            _userDataRepository = new UserDataRepository();
            _appCharaRepository = new ApplicantCharacteristicsRepository();
        }

        public ActionResult Index()
        {
            UserProfile currentUser = GetCurrentUser();
            UserRoleTypes currentUserRole = (UserRoleTypes)currentUser.RoleId;

            string redirectAction = "Index";

            switch (currentUserRole)
            {
                case UserRoleTypes.Applicant:
                    redirectAction = "SearchJobs";
                    break;
                case UserRoleTypes.Company:
                    redirectAction = "SearchApplicants";
                    break;
                case UserRoleTypes.Admin:
                    break;
                default:
                    break;
            }

            return RedirectToAction(redirectAction);
        }

        public ActionResult SearchJobs()
        {
            UserProfile currentUser = GetCurrentUser();
            UserData currentUserData = _userDataRepository.GetByUserId(currentUser.UserId);
            ApplicantCharacteristics userCharacteristics = _appCharaRepository.GetByApplicantId(currentUserData.Id);


            //TODO Make code refactoring and rework
            //Getting applicant abilities
            DataContext context = new DataContext();

            JobAppliances appliance = context.JobApplicances.Where(a => a.UserId == currentUser.UserId).FirstOrDefault();
            List<AbilitySets> absets = context.AblitySets.Where(a => a.ApplianceId == appliance.Id).ToList();

            List<Abilities> applicantAbilities = new List<Abilities>();

            foreach (AbilitySets abset in absets)
            {
                Abilities ability = context.Abilities.Find(abset.AbilitiesId);
                applicantAbilities.Add(ability);                
            }

            List<JobOffers> jobOffers = FindJobOffers(applicantAbilities, context);            
            
            return View(jobOffers);
        }

        public ActionResult SearchApplicants()
        {
            UserProfile currentUser = GetCurrentUser();

            //TODO Get Job offer id as an input parameter for search

            DataContext context = new DataContext();

            int offerId = 1;

            JobOffers currentOffer = context.JobOffers.Find(offerId);
            Professions currentProfession = context.Professions.Where(p => p.Id == currentOffer.ProfessionId).FirstOrDefault();

            var desiredSkills = context.AblitySets.Join(context.Abilities,
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

            var userAbilitySets = context.AblitySets.Where(aset => aset.ApplianceId != null && aset.ApplianceId > 0);

            var matchingApplianceIds = userAbilitySets.Join(context.Abilities,
                                    aset => aset.AbilitiesId,
                                    abil => abil.Id,
                                    (aset, abil) => new
                                    {
                                        SkillId = abil.SkillId,
                                        LevelId = abil.SkillLevelId,                                        
                                        ApplianceId = aset.ApplianceId
                                    }).
                                    Where(r => r.SkillId == desiredSkills.SkillId && r.LevelId == desiredSkills.SkillLevelId).
                                    Select(r => r.ApplianceId).
                                    ToList();
                
            
            return View();
        }



        private UserProfile GetCurrentUser()
        {
            //TODO Why this throws an exception?!
            //int currentUserId = WebSecurity.GetUserId(User.Identity.Name);
            //return _userRepository.GetById(currentUserId);
            
            return _userRepository.GetByName(User.Identity.Name);
        }

        private List<JobOffers> FindJobOffers(IEnumerable<Abilities> abilities, DataContext context)
        {
            var nonUserSkillsetsIds = context.AblitySets.Join(context.Abilities,
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
                                            Select(r => new {SkillId = r.skillId, SkillLevelId = r.sklvl, SkillSetId = r.setId});

            List<int> matchingSkillSets = abilities.Join(nonUserSkillsetsIds,
                                            a => a.SkillId,
                                            s => s.SkillId,
                                            (a, s) => new
                                            {
                                                setId = s.SkillSetId,
                                                OwnerSkillLvlId = a.SkillLevelId,
                                                DesiredSkillLvlId = s.SkillLevelId
                                            }).
                                            Where(r => r.OwnerSkillLvlId == r.DesiredSkillLvlId).
                                            Select(r => r.setId).
                                            ToList();

            List<JobOffers> matchingJobOffers = context.JobOffers.Join(context.Professions,
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
