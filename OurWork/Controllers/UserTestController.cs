using OurWork.Models;
using OurWork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OurWork.Controllers
{
    public class UserTestController : ApiController
    {
        private IRepository<UserProfile> _repository;

        public UserTestController()
        {
            _repository = new UserRepository();
        }

        #region Basic CRUD operations

        // GET api/usertest
        public IEnumerable<UserProfile> Get()
        {
            return _repository.GetAll();
        }

        // GET api/usertest/5
        public UserProfile Get(int id)
        {
            return _repository.GetById(id);
        }

        // POST api/usertest
        public void Post(UserProfile newUser)
        {
            if (_repository.Create(newUser))
            {
                _repository.Save();
            }
        }

        // PUT api/usertest
        public void Put(UserProfile user)
        {
            if (_repository.Update(user))
            {
                _repository.Save();
            }
        }

        // DELETE api/usertest/5
        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        #endregion
    }
}
