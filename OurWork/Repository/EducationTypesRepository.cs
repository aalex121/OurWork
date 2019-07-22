using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class EducationTypesRepository : IRepository<EducationType>
    {
        private readonly DataContext _context;

        public EducationTypesRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<EducationType> GetAll()
        {
            return _context.EducationTypes;
        }

        public EducationType GetById(int id)
        {
            return _context.EducationTypes.Find(id);
        }

        public bool Create(EducationType newUser)
        {
            if (!CheckEducationTypesId(newUser))
            {
                return false;
            }

            _context.EducationTypes.Add(newUser);

            return true;
        }

        public bool Update(EducationType user)
        {
            if (!CheckEducationTypesId(user))
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            EducationType user = _context.EducationTypes.Find(id);

            if (user != null)
            {
                _context.EducationTypes.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        private bool CheckEducationTypesId(EducationType user)
        {
            if (user.Id < 1 || _context.EducationTypes.Find(user.Id) == null)
            {
                return false;
            }

            return true;
        }

    }
}
