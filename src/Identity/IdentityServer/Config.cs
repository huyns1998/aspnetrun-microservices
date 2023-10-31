using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using static System.Formats.Asn1.AsnWriter;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //new Client
                //{
                //    ClientId = "catalogClient",
                //    ClientName = "Client Credentials Client",
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },
                //    AllowedScopes = { "catalogapi.read", "catalogapi.write" }
                //},
                new Client
                   {
                       ClientId = "mvc_client",
                       ClientName = "MVC Web App",
                       AllowedGrantTypes = GrantTypes.Hybrid,
                       RequirePkce = false,
                       //AllowRememberConsent = false,
                       RedirectUris = new List<string>()
                       {
                           "https://localhost:5013/signin-oidc"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:5013/signout-callback-oidc",
                       },
                       ClientSecrets = new List<Secret>
                       {
                           new Secret("secret".Sha256())
                       },
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           "catalogapi.read",
                           "ocelot",
                           IdentityServerConstants.StandardScopes.Email,
                           IdentityServerConstants.StandardScopes.Address,
                           "roles"
                       }
                   }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalogapi.read", "catalogapi read"),
                new ApiScope("catalogapi.write", "catalogapi write"),
                new ApiScope("ocelot", "ocelot")
            };

        public static IEnumerable<ApiResource> ApiResources => new[]
         {
      new ApiResource("catalogapi")
      {
        Scopes = new List<string> { "catalogapi.read", "catalogapi.write"},
        //ApiSecrets = new List<Secret> {new Secret(" ".Sha256())},
        //UserClaims = new List<string> {"role"}
      },
      new ApiResource("ocelot")
      {
          Scopes = new List<string>{"ocelot"}
      }
    };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile(),
                 new IdentityResources.Address(),
                 new IdentityResources.Email(),
                 new IdentityResource(
                     "roles",
                     "Your roles",
                     new List<string>(){"role"}
                     )
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                 new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "HuyNguyen",
                    Password = "swn",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "huy"),
                        new Claim(JwtClaimTypes.FamilyName, "nguyen")
                    }
                }
            };
    }
}
