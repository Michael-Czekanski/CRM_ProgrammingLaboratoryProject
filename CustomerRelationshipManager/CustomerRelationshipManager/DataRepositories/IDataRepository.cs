using CustomerRelationshipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.DataRepositories
{
    public interface IDataRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int ID);
        T Delete(int ID);
        T Add(T newObject);
        T Edit(T newData);

    }
}
