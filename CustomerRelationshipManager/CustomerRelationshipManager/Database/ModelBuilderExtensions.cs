using CustomerRelationshipManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Database
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            foreach(RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
            {
                modelBuilder.Entity<Role>().HasData( new Role() { ID = role, Name = role.ToString() });
            }

            modelBuilder.Entity<User>().HasData(new User()
            {
                Name = "John",
                ID = 1,
                DateOfBirth = new DateTime(1990, 2, 23),
                IsDeleted = false,
                Login = "John123",
                PasswordSHA256 = Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("JohnPassword"))),
                RoleID = RoleEnum.Admin,
                Surname = "Ross"
            });

        }
    }
}
