using CustomerRelationshipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.DataRepositories
{
    public interface ICompanyRepository: IDataRepository<Company>
    {
        Company FillBusinessIndustryNavProperty(Company companyToFillWithData);

        Company FillContactPeopleNavProperty(Company companyToFillWithData);

        Company FillBusinessNotesNavProperty(Company companyToFillWithData);
    }
}
