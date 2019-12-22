using System.Security.Cryptography;
using System.Text;

namespace CustomerRelationshipManager.Helpers
{
    public static class UserManagementHelper
    {
        public static string HashPasswordSHA256(string passwd)
        {
            return Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(passwd)));
        }

        
    }
}
