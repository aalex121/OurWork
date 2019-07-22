using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class AbilitySetsRepository : IRepository<AbilitySet>
    {
        private readonly DataContext _context;

        public AbilitySetsRepository()
        {
            _context = new DataContext();
        }
        
        public IEnumerable<AbilitySet> GetAll()
        {
            return _context.AblitySets;
        }

        public AbilitySet GetById(int id)
        {
            return _context.AblitySets.Find(id);
        }

        public bool Create(AbilitySet newRecord)
        {
            if (!CheckAppliance(newRecord))
            {
                return false;
            }

            _context.AblitySets.Add(newRecord);
            
            return true;
        }

        public bool Update(AbilitySet record)
        {
            //if (!CheckAppliance(record))
            //{
            //    return false;
            //}

            _context.Entry(record).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            AbilitySet current = _context.AblitySets.Find(id);

            if (current != null)
            {
                _context.AblitySets.Remove(current);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool CheckAppliance(AbilitySet record)
        {
            if (record.ApplianceId == 0)
            {
                return true;
            }

            return _context.JobApplicances.Find(record.ApplianceId) != null;
        }
    }
}