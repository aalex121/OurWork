using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using OurWork.Models;

namespace OurWork.Repository
{
    public class JobOffersRepository : IRepository<JobOffers>
    {
        private readonly DataContext _context;

        public JobOffersRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<JobOffers> GetAll()
        {
            return _context.JobOffers;
        }

        public JobOffers GetById(int id)
        {
            return _context.JobOffers.Find(id);
        }

        public bool Create(JobOffers newOffer)
        {
            if (!CheckUser(newOffer))
            {
                return false;
            }

            _context.JobOffers.Add(newOffer);

            return true;
        }

        public bool Update(JobOffers offer)
        {
            if (!CheckUser(offer))
            {
                return false;
            }
            
            _context.Entry(offer).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            JobOffers offer = _context.JobOffers.Find(id);

            if (offer != null)
            {
                _context.JobOffers.Remove(offer);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        private bool CheckUser(JobOffers offer)
        {
            if (offer.Id < 1 || _context.UserProfiles.Find(offer.UserId) == null)
            {
                return false;
            }

            return true;
        }
        
    }
}