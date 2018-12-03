using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class UserExtensions
    {
        public static bool IsManager(this ClaimsPrincipal user)
        {
            // a manager is an authenticated user with any permission claim (for now)

            if (!user.Identity.IsAuthenticated)
                return false;

            if (user.Claims.Any(x => x.Type.ToLower().Equals("permission")))
                return true;

            return false;
        }
    }
}
