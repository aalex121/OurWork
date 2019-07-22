using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using OurWork.Models;

namespace OurWork.Repository
{
    public class JobOffersRepository : IRepository<JobOffer>
    {
        private readonly DataContext _context;

        public JobOffersRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<JobOffer> GetAll()
        {
            return _context.JobOffers;
        }

        public JobOffer GetById(int id)
        {
            return _context.JobOffers.Find(id);
        }

        public IEnumerable<JobOffer> GetByUserId(int id)
        {
            return _context.JobOffers.Where(o => o.UserId == id).ToList();
        }

        public bool Create(JobOffer newOffer)
        {
            if (!CheckUserAndProfession(newOffer))
            {
                return false;
            }

            _context.JobOffers.Add(newOffer);

            return true;
        }

        public bool Update(JobOffer offer)
        {
            if (!CheckUserAndProfession(offer))
            {
                return false;
            }
            
            _context.Entry(offer).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            JobOffer offer = _context.JobOffers.Find(id);

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

        private bool CheckUserAndProfession(JobOffer offer)
        {
            bool fOk = offer.UserId >= 1 && _context.UserProfiles.Find(offer.UserId) != null;

            if (fOk)
            {
                fOk = offer.ProfessionId >= 1 && _context.Professions.Find(offer.ProfessionId) != null;
            }

            return fOk;
        }
        
    }
}