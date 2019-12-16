using CustomerRelationshipManager.Database;
using CustomerRelationshipManager.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.DataRepositories
{
    public class SQLUserRepository : IDataRepository<User>
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

        public User Delete(int ID)
        {
            User user = _context.Users.FirstOrDefault(u => u.ID == ID);

            if(user != null)
            {
                _context.Remove(user);
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
    }
}
