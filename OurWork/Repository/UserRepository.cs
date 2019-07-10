using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace OurWork.Repository
{
    public class UserRepository : IRepository<UserProfile>
    {
        private readonly DataContext _context;

        public UserRepository()
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
            if (!CheckRole(newUser))
            {
                return false;
            }
            
            _context.UserProfiles.Add(newUser);

            return true;
        }

        public bool Update(UserProfile user)
        {
            if (!CheckRole(user))
            {
                return false;
            }
            
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

        private bool CheckRole(UserProfile user)
        {
            if (user.RoleId < 1 || _context.UserRoles.Find(user.RoleId) == null)
            {
                return false;
            }

            return true;
        }
        
    }
}