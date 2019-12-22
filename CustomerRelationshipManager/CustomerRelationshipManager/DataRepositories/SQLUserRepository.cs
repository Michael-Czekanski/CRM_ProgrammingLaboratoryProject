using CustomerRelationshipManager.Database;
using CustomerRelationshipManager.Helpers;
using CustomerRelationshipManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CustomerRelationshipManager.DataRepositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public SQLUserRepository(AppDbContext context)
        {
            _context = context;
        }
        public User Add(User newObject)
        {
            newObject.PasswordSHA256 = Encoding.UTF8.GetString(
                SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(newObject.PasswordSHA256)));
            _context.Users.Add(newObject);
            _context.SaveChanges();
            return newObject;
        }

        public User Authenticate(string login, string password)
        {
            User user = _context.Users.FirstOrDefault(u => u.Login == login
            && u.PasswordSHA256 == UserManagementHelper.HashPasswordSHA256(password));

            if(user == null)
            {
                return null;
            }

            return user;
        }

        public User Delete(int ID)
        {
            User user = _context.Users.FirstOrDefault(u => u.ID == ID);

            if(user != null)
            {
                user.IsDeleted = true;
                Edit(user);
                _context.SaveChanges();
            }

            return user;
        }

        public User Edit(User newData)
        {
            EntityEntry<User> user = _context.Users.Attach(newData);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return newData;
        }

        public User Get(int ID)
        {
            return _context.Users.FirstOrDefault(u => u.ID == ID);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetAllAddedByUser(User user)
        {
            user.AddedCompanies = _context.Companies.Where(c => c.UserWhoAddedID == user.ID)
                .Include(c => c.BusinessIndustry).ToList();
            user.AddedContactPeople = _context.ContactPeople.Where(c => c.UserWhoAddedID == user.ID)
                .Include(c => c.Company).ToList();
            user.BusinessNotes = _context.BusinessNotes.Where(b => b.UserWhoAddedID == user.ID)
                .Include(b => b.Company).ToList();

            return user;

        }
    }
}
