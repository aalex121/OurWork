using OurWork.Repository;
using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _userRepository;

        public HomeController()
        {
            _userRepository = new UserRepository();
        }

        public ActionResult Index()
        {
            UserProfile user = null;
            
            if (Request.IsAuthenticated)
            {
                user = GetCurrentUser();
            }
            
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.User = user;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
