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

            IEnumerable<JobAppliances> userAppliances = _appRepository.GetByUserId(currentUser.UserId);

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

            ViewBag.Skills = skills;
            ViewBag.SkillLevels = skillLevels;
            ViewBag.SkillAttributes = skillAttributes;

            return View(dataModel);
        }

        [HttpPost]
        public ActionResult CreateAppliance(CreateApplianceModel data)
        {
            //Adding new appliance
            JobAppliances newAppliance = new JobAppliances();

            newAppliance.Name = data.ApplianceData.Name;
            newAppliance.Comment = data.ApplianceData.Comment;
            newAppliance.UserId = GetCurrentUser().UserId;

            if (!_appRepository.Create(newAppliance))
            {
                //TODO create error page and show it if operation fails
                return RedirectToAction("Index");
            }

            _appRepository.Save();

            int newAppId = newAppliance.Id;

            //Adding abilities for new appliance
            Abilities newAbilities = new Abilities();

            newAbilities.SkillId = data.ApplianceAbilities.SkillId;
            newAbilities.SkillLevelId = data.ApplianceAbilities.SkillLevelId;

            //TODO Implement adding multiple abilities!!!
            if (!_abilRepository.Create(newAbilities))
            {
                //TODO create error page and show it if operation fails
                return RedirectToAction("Index");
            }

            _abilRepository.Save();

            int newAbilId = newAbilities.Id;

            //Adding linking AbilitySet
            AbilitySets newAbilitySet = new AbilitySets();

            newAbilitySet.ApplianceId = newAppliance.Id;
            newAbilitySet.AbilitiesId = newAbilities.Id;

            if (!_abilSetRepository.Create(newAbilitySet))
            {
                //TODO create error page and show it if operation fails
                return RedirectToAction("Index");
            }

            _abilSetRepository.Save();

            return RedirectToAction("Index");
        }

        private UserProfile GetCurrentUser()
        {
            //TODO Why this throws an exception?!
            //int currentUserId = WebSecurity.GetUserId(User.Identity.Name);
            //return _userRepository.GetById(currentUserId);

            return _userRepository.GetByName(User.Identity.Name);
        }
    }
}
