using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Web.Controllers
{
    // http://benfoster.io/blog/customising-claims-transformation-in-aspnet-core-identity
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IGenericRepository<User> userRepository;

        public ClaimsTransformer(IGenericRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Gets called on every request - not just once on user create.
            string temp = string.Empty;
            var users = await userRepository.AllAsync();
            return principal;
        }
    }
}
