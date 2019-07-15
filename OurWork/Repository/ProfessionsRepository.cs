using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using OurWork.Models;

namespace OurWork.Repository
{
    public class ProfessionsRepository : IRepository<Professions>
    {
        private readonly DataContext _context;

        public ProfessionsRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<Professions> GetAll()
        {
            return _context.Professions;
        }

        public Professions GetById(int id)
        {
            return _context.Professions.Find(id);
        }

        public bool Create(Professions newProfession)
        {
            if (!CheckAbilitySet(newProfession))
            {
                return false;
            }

            _context.Professions.Add(newProfession);

            return true;
        }

        public bool Update(Professions profession)
        {
            if (!CheckAbilitySet(profession))
            {
                return false;
            }

            _context.Entry(profession).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            Professions profession = _context.Professions.Find(id);

            if (profession != null)
            {
                _context.Professions.Remove(profession);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        private bool CheckAbilitySet(Professions profession)
        {
            if (profession.Id < 1 || _context.AblitySets.Find(profession.AbilitySetId) == null) 
            {
                return false;
            }

            return true;
        }

    }
}