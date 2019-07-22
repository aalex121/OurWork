using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class AbilitiesRepository : IRepository<Ability>
    {
        private readonly DataContext _context;

        public AbilitiesRepository()
        {
            _context = new DataContext();
        }
        
        public IEnumerable<Ability> GetAll()
        {
            return _context.Abilities;
        }

        public Ability GetById(int id)
        {
            return _context.Abilities.Find(id);
        }

        public bool Create(Ability newRecord)
        {
            if (!CheckSkills(newRecord))
            {
                return false;
            }

            _context.Abilities.Add(newRecord);
            
            return true;
        }

        public bool Update(Ability record)
        {
            if (!CheckSkills(record))
            {
                return false;
            }

            _context.Entry(record).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            Ability current = _context.Abilities.Find(id);

            if (current != null)
            {
                _context.Abilities.Remove(current);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool CheckSkills(Ability record)
        {
            bool fOk = record.SkillId >= 1 && record.SkillLevelId >= 1;
            
            if (fOk)
            {
                fOk = _context.Skills.Find(record.SkillId) != null &&
                        _context.SkillLevels.Find(record.SkillLevelId) != null;
            }

            return fOk;
        }
    }
}