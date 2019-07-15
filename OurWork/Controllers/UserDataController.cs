using OurWork.Models;
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
    public class UserDataController : Controller
    {
        private readonly UserDataRepository _userDataRepository;
        private readonly UserRepository _userRepository;

        public UserDataController()
        {
            _userDataRepository = new UserDataRepository();
            _userRepository = new UserRepository();
        }

        public ActionResult Index()
        {
            UserProfile currentUser = GetCurrentUser();
            UserData currentUserData = _userDataRepository.GetByUserId(currentUser.UserId);

            ViewBag.CurrentUser = currentUser;
            ViewBag.FormAction = "UpdateUserData";

            if (currentUserData == null)
            {
                currentUserData = new UserData();
                currentUserData.UserId = currentUser.UserId;
                ViewBag.FormAction = "SetUserData";
            }       

            return View(currentUserData);
        }

        [HttpGet]
        public ActionResult ShowUserData(int id)
        {
            UserData currentUserData = _userDataRepository.GetByUserId(id);

            return View(currentUserData);
        }

        [HttpPost]
        public ActionResult SetUserData(UserData data)
        {
            data.BirthDate = DateTime.Now;
            int currentUserId = WebSecurity.GetUserId(User.Identity.Name);

            data.UserId = currentUserId;

            if (_userDataRepository.Create(data))
            {
                _userDataRepository.Save();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateUserData(UserData data)
        {
            data.BirthDate = DateTime.Now;
            data.UserId = GetCurrentUser().UserId;

            if (_userDataRepository.Update(data))
            {
                _userDataRepository.Save();
            }

            return RedirectToAction("Index");
        }

        #region Basic CRUD operations
        
        public IEnumerable<UserData> Get()
        {
            return _userDataRepository.GetAll();
        }
        
        public UserData Get(int id)
        {
            return _userDataRepository.GetById(id);
        }
       
        public void Post(UserData newUser)
        {
            if (_userDataRepository.Create(newUser))
            {
                _userDataRepository.Save();
            }
        }
        
        public void Put(UserData user)
        {
            if (_userDataRepository.Update(user))
            {
                _userDataRepository.Save();
            }
        }
        
        public void Delete(int id)
        {
            _userDataRepository.Delete(id);
            _userDataRepository.Save();
        }

        #endregion

        private UserProfile GetCurrentUser()
        {
            int currentUserId = WebSecurity.GetUserId(User.Identity.Name);
            return _userRepository.GetById(currentUserId);
        }
    }
}
