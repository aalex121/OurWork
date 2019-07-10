using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class RepositoryExample : IRepository<UserProfile>
    {
        private readonly DataContext _context;

        public RepositoryExample()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<UserProfile> GetAll()
        {
            return _context.UserProfiles;
        }

        public UserProfile GetById(int id)
        {
            return _context.UserProfiles.Find(id);
        }

        public bool Create(UserProfile newUser)
        {
            _context.UserProfiles.Add(newUser);

            return true;
        }

        public bool Update(UserProfile user)
        {
            _context.Entry(user).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            UserProfile user = _context.UserProfiles.Find(id);

            if (user != null)
            {
                _context.UserProfiles.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion



    }
}