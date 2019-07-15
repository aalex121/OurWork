using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class SkillsRepository : IRepository<Skills>
    {
        private readonly DataContext _context;

        public SkillsRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<Skills> GetAll()
        {
            return _context.Skills;
        }

        public Skills GetById(int id)
        {
            return _context.Skills.Find(id);
        }

        public bool Create(Skills record)
        {
            _context.Skills.Add(record);
            return true;
        }

        public bool Update(Skills record)
        {
            _context.Entry(record).State = EntityState.Modified;
            return true;
        }

        public void Delete(int id)
        {
            Skills record = _context.Skills.Find(id);

            if (record != null)
            {
                _context.Skills.Remove(record);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        public IEnumerable<SkillLevels> GetSkillLevels()
        {
            return _context.SkillLevels;
        }
    }
}