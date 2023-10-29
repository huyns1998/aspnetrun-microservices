using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.AuthrizationHandler
{
    public class ScopeRequirement : IAuthorizationRequirement
    {
        public string RequiredScope { get; }

        public ScopeRequirement(string requiredScope)
        {
            RequiredScope = requiredScope;
        }
    }
}