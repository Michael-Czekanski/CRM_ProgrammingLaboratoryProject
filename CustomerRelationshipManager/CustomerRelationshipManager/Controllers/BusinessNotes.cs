using CustomerRelationshipManager.DataRepositories;
using CustomerRelationshipManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Controllers
{
    public class BusinessNotes: Controller
    {
        private readonly IBusinessNoteRepository _businessNoteRepository;

        public BusinessNotes(IBusinessNoteRepository businessNoteRepository)
        {
            _businessNoteRepository = businessNoteRepository;
        }
    }
}
