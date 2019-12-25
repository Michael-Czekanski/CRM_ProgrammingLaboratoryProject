using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CustomerRelationshipManager.CustomAttributes
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;

        public AuthorizeFilter(params string[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            ClaimsIdentity claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (IsAuthenticated)
            {
                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    if (context.HttpContext.User.HasClaim(ClaimTypes.Role, item))
                        flagClaim = true;
                }
                if (!flagClaim)
                {
                    context.Result = new UnauthorizedObjectResult("No permission");
                }
            }
            else
            {
                context.Result = new RedirectResult("~/Home/Index");
            }
            return;
        }
    }
}
