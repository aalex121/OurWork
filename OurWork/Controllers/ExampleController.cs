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
    public class ExampleController : ApiController
    {
        private IRepository<UserProfile> _repository;

        public ExampleController()
        {
            _repository = new UserRepository();
        }

        #region Basic CRUD operations

        // GET api/example
        public IEnumerable<UserProfile> Get()
        {
            return _repository.GetAll();
        }

        // GET api/example/5
        public UserProfile Get(int id)
        {
            return _repository.GetById(id);
        }

        // POST api/example
        public void Post(UserProfile newUser)
        {
            _repository.Create(newUser);
            _repository.Save();
        }

        // PUT api/example
        public void Put(UserProfile user)
        {
            _repository.Update(user);
            _repository.Save();
        }

        // DELETE api/example/5
        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        #endregion
    }
}
