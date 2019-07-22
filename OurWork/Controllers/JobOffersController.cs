using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using OurWork.Models;
using OurWork.Repository;

namespace OurWork.Controllers
{
    public class JobOffersController : ApiController
    {
        private IRepository<JobOffer> _repository;

        public JobOffersController()
        {
            _repository = new JobOffersRepository();
        }

        #region Basic CRUD operations

        // GET api/usertest
        public IEnumerable<JobOffer> Get()
        {
            return _repository.GetAll();
        }

        // GET api/usertest/5
        public JobOffer Get(int id)
        {
            return _repository.GetById(id);
        }

        // POST api/usertest
        public void Post(JobOffer newOffer)
        {
            if (_repository.Create(newOffer))
            {
                _repository.Save();
            }
        }

        // PUT api/usertest
        public void Put(JobOffer offer)
        {
            if (_repository.Update(offer))
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
