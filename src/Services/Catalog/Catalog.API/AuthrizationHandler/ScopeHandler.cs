using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.AuthrizationHandler
{
    public class ScopeHandler : AuthorizationHandler<ScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains(requirement.RequiredScope)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
