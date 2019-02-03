using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Web.Identity
{
    //http://benfoster.io/blog/customising-claims-transformation-in-aspnet-core-identity
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        private UserManager<User> userManager;
        private readonly IGenericRepository<Role> roleRepository;
        private readonly IGenericRepository<Permission> permissionRepository;
        private readonly IOptions<IdentityOptions> optionsAccessor;

        public ApplicationUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            IGenericRepository<Role> roleRepository,
            IGenericRepository<Permission> permissionRepository,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            this.userManager = userManager;
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
            this.optionsAccessor = optionsAccessor;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var claimsUser = await base.GenerateClaimsAsync(user);

            if (user.RoleID.HasValue)
            {
                var role = await roleRepository.FindByIdAsync(
                    user.RoleID.Value, 
                    x => x.RolePermissions);

                // TODO Update this crazy code... Incorporate ThenInclude into0 ?

                var allPermissions = await permissionRepository.AllAsync();

                foreach(var permission in allPermissions)
                {
                    if (role.RolePermissions.Select(x => x.PermissionID)
                        .Contains(permission.PermissionID))
                    {
                        claimsUser.AddClaim(new Claim("permission", $"{permission.Name}"));
                    }
                }
            }
            return claimsUser;
        }
    }
}
