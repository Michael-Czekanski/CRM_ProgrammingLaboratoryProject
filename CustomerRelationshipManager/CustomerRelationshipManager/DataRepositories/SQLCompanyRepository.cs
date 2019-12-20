using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRelationshipManager.Database;
using CustomerRelationshipManager.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerRelationshipManager.DataRepositories
{
    public class SQLCompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public SQLCompanyRepository(AppDbContext context)
        {
            _context = context;
        }
        public Company Add(Company newObject)
        {
            _context.Companies.Add(newObject);
            _context.SaveChanges();
            return newObject;
        }

        public Company Delete(int ID)
        {
            Company company = _context.Companies.FirstOrDefault(c => c.ID == ID);

            if (company != null)
            {
                company.IsDeleted = true;
                Edit(company);
                _context.SaveChanges();
            }

            return company;
        }

        public Company Edit(Company newData)
        {
            EntityEntry<Company> company = _context.Companies.Attach(newData);
            company.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return newData;
        }

        public Company FillBusinessIndustryNavProperty(Company companyToFillWithData)
        {
            companyToFillWithData.BusinessIndustry = _context.BusinessIndustries
                .FirstOrDefault(b => b.ID == companyToFillWithData.BusinessIndustryID);

            return companyToFillWithData;
        }

        public Company FillBusinessNotesNavProperty(Company companyToFillWithData)
        {
            companyToFillWithData.BusinessNotes = _context.BusinessNotes
                .Where(b => b.CompanyID == companyToFillWithData.ID);

            return companyToFillWithData;
        }

        public Company FillContactPeopleNavProperty(Company companyToFillWithData)
        {
            companyToFillWithData.ContactPeople = _context.ContactPeople
                .Where(c => c.CompanyID == companyToFillWithData.ID);

            return companyToFillWithData;
        }

        public Company Get(int ID)
        {
            return _context.Companies.FirstOrDefault(c => c.ID == ID);
        }

        public IEnumerable<Company> GetAll()
        {
            return _context.Companies;
        }
    }
}
