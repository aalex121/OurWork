using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OurWork.Models;
using OurWork.Repository;

namespace OurWork.Controllers
{
    public class CreateVacancyController : Controller
    {
        private readonly JobOffersRepository _jobOfferRepo;
        private readonly UserRepository _userRepo;
        private readonly ProfessionsRepository _profRepo;
        private readonly SkillsRepository _skillsRepository;
        private readonly AbilitiesRepository _abilRepository;
        private readonly AbilitySetsRepository _abilSetRepository;

        public CreateVacancyController()
        {
            _jobOfferRepo = new JobOffersRepository();
            _userRepo = new UserRepository();
            _profRepo = new ProfessionsRepository();
            _skillsRepository = new SkillsRepository();
            _abilRepository = new AbilitiesRepository();
            _abilSetRepository = new AbilitySetsRepository();
        }

        public ActionResult Index()
        {
            JobOffer newOffer = new JobOffer();

            List<SelectListItem> professions = _profRepo.GetAll().
                        Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).
                        ToList();

            ViewBag.Professions = professions;

            return View(newOffer);
        }

        public ActionResult NewProfession()
        {
            CreateProfessionModel newProfession = new CreateProfessionModel();

            List<SelectListItem> skills = _skillsRepository.GetAll().
                        Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.SkillName }).
                        ToList();

            List<SelectListItem> skillLevels = _skillsRepository.GetSkillLevels().
                        Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.ValueName }).
                        ToList();

            ViewBag.Skills = skills;
            ViewBag.SkillLevels = skillLevels;

            return View(newProfession);
        }

        [HttpPost]
        public ActionResult CreateVacancy(JobOffer offer)
        {
            offer.PublishDate = DateTime.Now;

            offer.UserId = GetCurrentUser().UserId;            

            if (_jobOfferRepo.Create(offer))
            {
                _jobOfferRepo.Save();
            }

            return Redirect("/Vacancy/vacancy/" + offer.Id);
        }

        public ActionResult CreateProfession(CreateProfessionModel dataModel)
        {
            Ability newAbilities = new Ability();
            newAbilities.SkillId = dataModel.SkillId;
            newAbilities.SkillLevelId = dataModel.SkillLevelId;

            if (_abilRepository.Create(newAbilities))
            {
                _abilRepository.Save();
            }            

            if (newAbilities.Id == 0)
            {
                //TODO Repalce with error page
                return Redirect("/Home/Index");
            }

            AbilitySet newAbilSet = new AbilitySet();
            newAbilSet.AbilitiesId = newAbilities.Id;
            newAbilSet.ApplianceId = 0;

            if (_abilSetRepository.Create(newAbilSet))
            {
                _abilSetRepository.Save();
            }

            if (newAbilSet.Id == 0)
            {
                //TODO Repalce with error page
                return Redirect("/Home/Index");
            }

            Profession newProfession = new Profession();
            newProfession.Name = dataModel.ProfessionData.Name;
            newProfession.AbilitySetId = newAbilSet.Id;

            if (_profRepo.Create(newProfession))
            {
                _profRepo.Save();
            }

            return Redirect("/CreateVacancy/Index");
        }

        private UserProfile GetCurrentUser()
        {
            return _userRepo.GetByName(User.Identity.Name);
        }

    }
}
