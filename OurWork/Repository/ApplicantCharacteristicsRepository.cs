using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class ApplicantCharacteristicsRepository : IRepository<ApplicantCharacteristics>
    {
        private readonly DataContext _context;

        public ApplicantCharacteristicsRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<ApplicantCharacteristics> GetAll()
        {
            return _context.ApplicantCharacteristics;
        }
        
        public ApplicantCharacteristics GetById(int id)
        {
            return _context.ApplicantCharacteristics.Find(id);
        }

        public ApplicantCharacteristics GetByApplicantId(int appId)
        {
            return _context.ApplicantCharacteristics.Where(c => c.ApplicantId == appId).FirstOrDefault();
        }
        
        public bool Create(ApplicantCharacteristics newUser)
        {
            if (!CheckApplicantId(newUser))
            {
                return false;
            }

            _context.ApplicantCharacteristics.Add(newUser);

            return true;
        }
        ///////
        public bool Update(ApplicantCharacteristics user)
        {
            if (!CheckApplicantId(user))
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            ApplicantCharacteristics user = _context.ApplicantCharacteristics.Find(id);

            if (user != null)
            {
                _context.ApplicantCharacteristics.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        private bool CheckApplicantId(ApplicantCharacteristics user)
        {
            if (user.Id < 1 || _context.ApplicantCharacteristics.Find(user.Id) == null)
            {
                return false;
            }

            return true;
        }

    }
}
