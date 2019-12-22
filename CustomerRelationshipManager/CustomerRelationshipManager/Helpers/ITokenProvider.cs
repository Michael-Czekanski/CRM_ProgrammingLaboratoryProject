using CustomerRelationshipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManager.Helpers
{
    public interface ITokenProvider
    {
        string ProvideToken(User user);
    }
}
