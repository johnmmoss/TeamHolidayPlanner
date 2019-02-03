using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHolidayPlanner.Data;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Web.Identity
{
    public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly IServiceProvider serviceProvider;

        public PermissionPolicyProvider(
            IOptions<AuthorizationOptions> options
            ) : base(options)
        {
        }

        public async override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var current = await base.GetPolicyAsync(policyName); 

            // Add some sexy cool policy reconciliation

            return current;
        }
    }
}
