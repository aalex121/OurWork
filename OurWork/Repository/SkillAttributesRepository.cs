using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class SkillAttributesRepository : IRepository<SkillAttribute>
    {
        private readonly DataContext _context;

        public SkillAttributesRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<SkillAttribute> GetAll()
        {
            return _context.SkillAttributes;
        }

        public SkillAttribute GetById(int id)
        {
            return _context.SkillAttributes.Find(id);
        }

        public bool Create(SkillAttribute record)
        {
            if (!CheckSkill(record))
            {
                return false;
            }
            
            _context.SkillAttributes.Add(record);

            return true;
        }

        public bool Update(SkillAttribute record)
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
            SkillAttribute record = _context.SkillAttributes.Find(id);

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

        private bool CheckSkill(SkillAttribute record)
        {
            if (record.SkillId < 1 || _context.Skills.Find(record.SkillId) == null)
            {
                return false;
            }

            return true;
        }
    }
}