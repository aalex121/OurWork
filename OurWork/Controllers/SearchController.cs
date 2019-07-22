using OurWork.Enums;
using OurWork.Models;
using OurWork.Repository;
using OurWork.SearchLogic;
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
        private const int SEARCH_SKILL_SPREAD_VALUE = 25;
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

            JobFinder search = new JobFinder();
            List<JobOffer> jobOffers = search.SearchJobs(currentUser);            
            
            return View(jobOffers);
        }
        
        public ActionResult SearchApplicants(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index");
            }

            int offerId = (int)id;            
            UserProfile currentUser = GetCurrentUser();
            ApplicantFinder search = new ApplicantFinder(SEARCH_SKILL_SPREAD_VALUE);
            List<ApplicantSearchResults> results = search.SearchApplicants(offerId);

            return View(results);
        }

        private UserProfile GetCurrentUser()
        {
            return _userRepository.GetByName(User.Identity.Name);
        }
    }
}
