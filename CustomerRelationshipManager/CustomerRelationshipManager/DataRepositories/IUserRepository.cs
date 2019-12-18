using CustomerRelationshipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.DataRepositories
{
    public interface IUserRepository: IDataRepository<User>
    {
        /// <summary>
        /// Gets all companies, contact people and business notes added by user.
        /// Sets this values to proper properties.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User GetAllAddedByUser(User user);
    }
}
