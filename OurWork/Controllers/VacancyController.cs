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
    public class VacancyController : Controller
    {
        private readonly JobOffersRepository _jobOfferRepo;
        private readonly UserRepository _userRepo;
        private readonly UserDataRepository _userDatarepo;

        public VacancyController()
        {
            _jobOfferRepo = new JobOffersRepository();
            _userRepo = new UserRepository();
            _userDatarepo = new UserDataRepository();
        }

        public ActionResult Index()
        {
            UserProfile currentUser = GetCurrentUser(); 

            if (!User.Identity.IsAuthenticated || (currentUser == null))
	        {
                return Redirect("/Account/Login");
	        }

            if (currentUser.RoleId != (int)UserRoleTypes.Company)
            {
                return Redirect("/Vacancy/WrongRole");
            }  

            int userId = GetCurrentUser().UserId;

            IEnumerable<JobOffer> offers = _jobOfferRepo.GetByUserId(userId);

            return View(offers);
        }

        public ActionResult AllVacancies()
        {
            IEnumerable<JobOffer> AllOffers = _jobOfferRepo.GetAll();

            return View(AllOffers);
        }

        public ActionResult WrongRole()
        {
           return View();
        }

        public ActionResult Vacancy(int? id)
        {
            if (id == null)
            {
                return Redirect("/Error/Index");
            }

            int vacancyId = (int)id;

            JobOffer currentOffer = _jobOfferRepo.GetById(vacancyId);            
            UserData companyData = _userDatarepo.GetByUserId(currentOffer.UserId);
            
            ViewBag.CurrentUser = GetCurrentUser();
            ViewBag.CompanyData = companyData;

            return View(currentOffer);
        }       

        private UserProfile GetCurrentUser()
        {
            return _userRepo.GetByName(User.Identity.Name);
        }

    }
}
