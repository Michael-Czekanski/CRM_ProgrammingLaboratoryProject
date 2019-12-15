using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRelationshipManager.Database;
using CustomerRelationshipManager.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerRelationshipManager.DataRepositories
{
    public class SQLContactPersonRepository: IDataRepository<ContactPerson>
    {
        private readonly AppDbContext _context;

        public SQLContactPersonRepository(AppDbContext context)
        {
            _context = context;
        }
        public ContactPerson Add(ContactPerson newObject)
        {
            _context.ContactPeople.Add(newObject);
            _context.SaveChanges();
            return newObject;
        }

        public ContactPerson Delete(int ID)
        {
            ContactPerson contactPerson = _context.ContactPeople.FirstOrDefault(c => c.ID == ID);

            if (contactPerson != null)
            {
                _context.Remove(contactPerson);
                _context.SaveChanges();
            }

            return contactPerson;
        }

        public ContactPerson Edit(ContactPerson newData)
        {
            EntityEntry<ContactPerson> contactPerson = _context.ContactPeople.Attach(newData);
            contactPerson.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return newData;
        }

        public ContactPerson Get(int ID)
        {
            return _context.ContactPeople.FirstOrDefault(c => c.ID == ID);
        }

        public IEnumerable<ContactPerson> GetAll()
        {
            return _context.ContactPeople;
        }
    }
}
