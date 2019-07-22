using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using OurWork.Models;

namespace OurWork.Repository
{
    public class ProfessionsRepository : IRepository<Profession>
    {
        private readonly DataContext _context;

        public ProfessionsRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<Profession> GetAll()
        {
            return _context.Professions;
        }

        public Profession GetById(int id)
        {
            return _context.Professions.Find(id);
        }

        public bool Create(Profession newProfession)
        {
            if (!CheckAbilitySet(newProfession))
            {
                return false;
            }

            _context.Professions.Add(newProfession);

            return true;
        }

        public bool Update(Profession profession)
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
            Profession profession = _context.Professions.Find(id);

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

        private bool CheckAbilitySet(Profession profession)
        {
            return profession.AbilitySetId > 1 && _context.AblitySets.Find(profession.AbilitySetId) != null;
        }

    }
}