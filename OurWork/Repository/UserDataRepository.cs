using OurWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurWork.Repository
{
    public class UserDataRepository : IRepository<UserData>
    {
        private readonly DataContext _context;

        public UserDataRepository()
        {
            _context = new DataContext();
        }

        #region Basic CRUD operations

        public IEnumerable<UserData> GetAll()
        {
            return _context.UserData;
        }

        public UserData GetById(int id)
        {
            return _context.UserData.Find(id);
        }

        public UserData GetByUserId(int userId)
        {
            return _context.UserData.Where(d => d.UserId == userId).FirstOrDefault();
        }

        public bool Create(UserData newUser)
        {
            if (!CheckUserId(newUser))
            {
                return false;
            }

            _context.UserData.Add(newUser);

            return true;
        }

        public bool Update(UserData newData)
        {
            UserData currentUserData = _context.UserData.Where(d => d.UserId == newData.UserId).FirstOrDefault();

            if (!CheckUserId(newData) || currentUserData == null)
            {
                return false;
            }

            currentUserData.FirstName = newData.FirstName;
            currentUserData.SecondName = newData.SecondName;
            currentUserData.BirthDate = newData.BirthDate;
            currentUserData.CompanyName = newData.CompanyName;
            currentUserData.Email = newData.Email;
            currentUserData.Phone = newData.Phone;

            _context.Entry(currentUserData).State = EntityState.Modified;

            return true;
        }

        public void Delete(int id)
        {
            UserData user = _context.UserData.Find(id);

            if (user != null)
            {
                _context.UserData.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();            
        }

        #endregion

        private bool CheckUserId(UserData user)
        {
            if (user.UserId < 1 || _context.UserProfiles.Find(user.UserId) == null)
            {
                return false;
            }

            return true;
        }

    }
}
