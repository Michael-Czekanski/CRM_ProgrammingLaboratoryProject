using CustomerRelationshipManager.Database;
using CustomerRelationshipManager.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.DataRepositories
{
    public class SQLBusinessIndustryRepository : IDataRepository<BusinessIndustry>
    {
        private readonly AppDbContext _context;

        public SQLBusinessIndustryRepository(AppDbContext context)
        {
            _context = context;
        }
        public BusinessIndustry Add(BusinessIndustry newObject)
        {
            _context.BusinessIndustries.Add(newObject);
            _context.SaveChanges();
            return newObject;
        }

        public BusinessIndustry Delete(int ID)
        {
            BusinessIndustry businessIndustry = _context.BusinessIndustries.FirstOrDefault(b => (int)b.ID == ID);

            if (businessIndustry != null)
            {
                _context.Remove(businessIndustry);
                _context.SaveChanges();
            }

            return businessIndustry;
        }

        public BusinessIndustry Edit(BusinessIndustry newData)
        {
            EntityEntry<BusinessIndustry> businessIndustry = _context.BusinessIndustries.Attach(newData);
            businessIndustry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return newData;
        }

        public BusinessIndustry Get(int ID)
        {
            return _context.BusinessIndustries.FirstOrDefault(b => (int)b.ID == ID);
        }

        public IEnumerable<BusinessIndustry> GetAll()
        {
            return _context.BusinessIndustries;
        }
    }
}
