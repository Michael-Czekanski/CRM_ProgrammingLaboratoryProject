using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRelationshipManager.Database;
using CustomerRelationshipManager.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerRelationshipManager.DataRepositories
{
    public class SQLBusinessNoteRepository : IDataRepository<BusinessNote>
    {
        private readonly AppDbContext _context;

        public SQLBusinessNoteRepository(AppDbContext context)
        {
            _context = context;
        }
        public BusinessNote Add(BusinessNote newObject)
        {
            _context.BusinessNotes.Add(newObject);
            _context.SaveChanges();
            return newObject;
        }

        public BusinessNote Delete(int ID)
        {
            BusinessNote businessNote = _context.BusinessNotes.FirstOrDefault(b => b.ID == ID);

            if (businessNote != null)
            {
                businessNote.IsDeleted = true;
                Edit(businessNote);
                _context.SaveChanges();
            }

            return businessNote;
        }

        public BusinessNote Edit(BusinessNote newData)
        {
            EntityEntry<BusinessNote> businessNote = _context.BusinessNotes.Attach(newData);
            businessNote.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return newData;
        }

        public BusinessNote Get(int ID)
        {
            return _context.BusinessNotes.FirstOrDefault(b => b.ID == ID);
        }

        public IEnumerable<BusinessNote> GetAll()
        {
            return _context.BusinessNotes;
        }
    }
}
