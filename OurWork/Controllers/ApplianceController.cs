using OurWork.Models;
using OurWork.Enums;
using OurWork.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace OurWork.Controllers
{
    public class ApplianceController : Controller
    {
        private const int DEFAULT_APPLIANCE_ABILITIES = 2;
        private readonly JobAppliancesRepository _appRepository;
        private readonly UserRepository _userRepository;
        private readonly SkillsRepository _skillsRepository;
        private readonly SkillAttributesRepository _skillAttrsRepository;
        private readonly AbilitiesRepository _abilRepository;
        private readonly AbilitySetsRepository _abilSetRepository;

        public ApplianceController()
        {
            _appRepository = new JobAppliancesRepository();
            _userRepository = new UserRepository();
            _skillsRepository = new SkillsRepository();
            _skillAttrsRepository = new SkillAttributesRepository();
            _abilRepository = new AbilitiesRepository();
            _abilSetRepository = new AbilitySetsRepository();
        }

        public ActionResult Index()
        {
            UserProfile currentUser = GetCurrentUser();

            if (currentUser.RoleId != (int)UserRoleTypes.Applicant)
            {
                return Redirect("/Home/Index");
            }

            IEnumerable<JobAppliance> userAppliances = _appRepository.GetByUserId(currentUser.UserId);

            return View(userAppliances);
        }

        public ActionResult NewAppliance()
        {
            UserProfile currentUser = GetCurrentUser();

            if (currentUser.RoleId != (int)UserRoleTypes.Applicant)
            {
                return Redirect("/Home/Index");
            }                       

            List<SelectListItem> skills = _skillsRepository.GetAll().
                        Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.SkillName }).
                        ToList();            

            List<SelectListItem> skillLevels = _skillsRepository.GetSkillLevels().
                        Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.ValueName }).
                        ToList();            

            List<SelectListItem> skillAttributes = _skillAttrsRepository.GetAll().
                        Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).
                        ToList();

            CreateApplianceModel dataModel = new CreateApplianceModel();

            for (int i = 0; i < DEFAULT_APPLIANCE_ABILITIES; i++)
            {
                Ability ability = new Ability();
                dataModel.ApplianceAbilities.Add(ability);
            }

            ViewBag.Skills = skills;
            ViewBag.SkillLevels = skillLevels;
            ViewBag.SkillAttributes = skillAttributes;

            return View(dataModel);
        }

        [HttpPost]
        public ActionResult CreateAppliance(CreateApplianceModel data)
        {
            //Adding new appliance
            JobAppliance newAppliance = new JobAppliance();

            newAppliance.Name = data.ApplianceData.Name;
            newAppliance.Comment = data.ApplianceData.Comment;
            newAppliance.UserId = GetCurrentUser().UserId;

            if (!_appRepository.Create(newAppliance))
            {                
                return RedirectToAction("/Error/Index");
            }

            _appRepository.Save();

            int newAppId = newAppliance.Id;

            //Adding abilities and ability set for new appliance
            foreach (Ability ability in data.ApplianceAbilities)
            {                
                Ability newAbilities = new Ability();

                newAbilities.SkillId = ability.SkillId;
                newAbilities.SkillLevelId = ability.SkillLevelId;
                
                if (!_abilRepository.Create(newAbilities))
                {
                    return RedirectToAction("/Error/Index");
                }

                _abilRepository.Save();

                int newAbilId = newAbilities.Id;
                
                AbilitySet newAbilitySet = new AbilitySet();

                newAbilitySet.ApplianceId = newAppliance.Id;
                newAbilitySet.AbilitiesId = newAbilities.Id;

                if (!_abilSetRepository.Create(newAbilitySet))
                {
                    return RedirectToAction("/Error/Index");
                }

                _abilSetRepository.Save();
            }

            return RedirectToAction("Index");
        }

        private UserProfile GetCurrentUser()
        {
            return _userRepository.GetByName(User.Identity.Name);
        }
    }
}
