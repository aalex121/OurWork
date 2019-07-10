using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class SkillAttributesRepository : IRepository<SkillAttributes>
    {
        private readonly DataContext _context;

        public SkillAttributesRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<SkillAttributes> GetAll()
        {
            return _context.SkillAttributes;
        }

        public SkillAttributes GetById(int id)
        {
            return _context.SkillAttributes.Find(id);
        }

        public bool Create(SkillAttributes record)
        {
            if (!CheckSkill(record))
            {
                return false;
            }
            
            _context.SkillAttributes.Add(record);

            return true;
        }

        public bool Update(SkillAttributes record)
        {
            if (!CheckSkill(record))
            {
                return false;
            }
            
            _context.Entry(record).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            SkillAttributes record = _context.SkillAttributes.Find(id);

            if (record != null)
            {
                _context.SkillAttributes.Remove(record);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        private bool CheckSkill(SkillAttributes record)
        {
            if (record.SkillId < 1 || _context.Skills.Find(record.SkillId) == null)
            {
                return false;
            }

            return true;
        }
    }
}