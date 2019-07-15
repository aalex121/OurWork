using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class EducationTypesRepository : IRepository<EducationTypes>
    {
        private readonly DataContext _context;

        public EducationTypesRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<EducationTypes> GetAll()
        {
            return _context.EducationTypes;
        }

        public EducationTypes GetById(int id)
        {
            return _context.EducationTypes.Find(id);
        }

        public bool Create(EducationTypes newUser)
        {
            if (!CheckEducationTypesId(newUser))
            {
                return false;
            }

            _context.EducationTypes.Add(newUser);

            return true;
        }

        public bool Update(EducationTypes user)
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
            EducationTypes user = _context.EducationTypes.Find(id);

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

        private bool CheckEducationTypesId(EducationTypes user)
        {
            if (user.Id < 1 || _context.EducationTypes.Find(user.Id) == null)
            {
                return false;
            }

            return true;
        }

    }
}
