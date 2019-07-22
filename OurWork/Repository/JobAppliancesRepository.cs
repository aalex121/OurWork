using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class JobAppliancesRepository : IRepository<JobAppliance>
    {
        private readonly DataContext _context;

        public JobAppliancesRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<JobAppliance> GetAll()
        {
            return _context.JobApplicances;
        }

        public JobAppliance GetById(int id)
        {
            return _context.JobApplicances.Find(id);
        }

        public IEnumerable<JobAppliance> GetByUserId(int userId)
        {
            return _context.JobApplicances.Where(a => a.UserId == userId).ToList();
        }

        public bool Create(JobAppliance record)
        {
            if (!CheckUser(record))
            {
                return false;
            }

            _context.JobApplicances.Add(record);

            return true;
        }

        public bool Update(JobAppliance user)
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
            JobAppliance user = _context.JobApplicances.Find(id);

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

        private bool CheckUser(JobAppliance appliance)
        {
            return appliance.UserId >= 1 && _context.UserProfiles.Find(appliance.UserId) != null;
        }

    }
}
