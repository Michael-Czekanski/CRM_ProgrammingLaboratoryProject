using CustomerRelationshipManager.DataRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    public class ContactPeopleController
    {
        private readonly IContactPersonRepository _contactPersonRepository;
        public ContactPeopleController(IContactPersonRepository contactPersonRepository)
        {
            _contactPersonRepository = contactPersonRepository;
        }
    }
}
