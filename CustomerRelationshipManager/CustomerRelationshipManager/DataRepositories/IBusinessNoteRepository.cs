﻿using CustomerRelationshipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.DataRepositories
{
    public interface IBusinessNoteRepository: IDataRepository<BusinessNote>
    {
        BusinessNote FillCompanyNavProperty(BusinessNote businessNoteToFillWithData);
    }
}
