using Microsoft.AspNetCore.Http;
using System;
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

        public static bool TryGetLoggedUserID(HttpContext context, out int varToPutID)
        {
            byte[] loggedInUserIDBytes;
            int loggedInUserID;
            if (context.Session.TryGetValue("UserID", out loggedInUserIDBytes))
            {
                loggedInUserID = BitConverter.ToInt32(loggedInUserIDBytes, 0);
                varToPutID = loggedInUserID;
                return true;
            }
            else
            {
                varToPutID = -1;
                return false;
            }
        }
        
    }
}
