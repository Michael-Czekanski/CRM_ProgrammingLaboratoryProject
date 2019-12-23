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
            foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
            {
                modelBuilder.Entity<Role>().HasData(new Role() { ID = role, Name = role.ToString() });
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

            foreach (BusinessIndustryEnum businessIndustry in Enum.GetValues(typeof(BusinessIndustryEnum)))
            {
                modelBuilder.Entity<BusinessIndustry>()
                    .HasData(new BusinessIndustry() { ID = businessIndustry, Name = businessIndustry.ToString() });
            }


            modelBuilder.Entity<Company>().HasData(new Company()
            {
                ID = 1,
                Name = "Sample Company",
                BusinessIndustryID = BusinessIndustryEnum.Computer,
                Address = "Sample Address",
                City = "Sample City",
                UserWhoAddedID = 1,
                NIP = "1234567890",
                DateAdded = DateTime.Now,
                IsDeleted = false
            });

            modelBuilder.Entity<ContactPerson>().HasData(new ContactPerson()
            {
                ID = 1,
                CompanyID = 1,
                UserWhoAddedID = 1,
                Name = "Bob",
                Surname = "John",
                Email = "bob.john@fmail.com",
                Position = "Sample",
                TelephoneNum = "123456789",
                IsDeleted = false
            });

            modelBuilder.Entity<BusinessNote>().HasData(new BusinessNote()
            {
                ID = 1,
                Content = "Sample business note",
                CompanyID = 1,
                UserWhoAddedID = 1,
                IsDeleted = false
            });
        }
    }
}
