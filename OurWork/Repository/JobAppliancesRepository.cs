using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class JobAppliancesRepository : IRepository<JobAppliances>
    {
        private readonly DataContext _context;

        public JobAppliancesRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<JobAppliances> GetAll()
        {
            return _context.JobApplicances;
        }

        public JobAppliances GetById(int id)
        {
            return _context.JobApplicances.Find(id);
        }

        public IEnumerable<JobAppliances> GetByUserId(int userId)
        {
            return _context.JobApplicances.Where(a => a.UserId == userId).ToList();
        }

        public bool Create(JobAppliances record)
        {
            if (!CheckUser(record))
            {
                return false;
            }

            _context.JobApplicances.Add(record);

            return true;
        }

        public bool Update(JobAppliances user)
        {
            if (!CheckUser(user))
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            JobAppliances user = _context.JobApplicances.Find(id);

            if (user != null)
            {
                _context.JobApplicances.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        private bool CheckUser(JobAppliances appliance)
        {
            return appliance.UserId >= 1 && _context.UserProfiles.Find(appliance.UserId) != null;
        }

    }
}
