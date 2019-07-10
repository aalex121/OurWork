using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class AbilitiesRepository : IRepository<Abilities>
    {
        private readonly DataContext _context;

        public AbilitiesRepository()
        {
            _context = new DataContext();
        }
        
        public IEnumerable<Abilities> GetAll()
        {
            return _context.Abilities;
        }

        public Abilities GetById(int id)
        {
            return _context.Abilities.Find(id);
        }

        public bool Create(Abilities newRecord)
        {
            if (!CheckSkills(newRecord))
            {
                return false;
            }

            _context.Abilities.Add(newRecord);
            
            return true;
        }

        public bool Update(Abilities record)
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
            Abilities current = _context.Abilities.Find(id);

            if (current != null)
            {
                _context.Abilities.Remove(current);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool CheckSkills(Abilities record)
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