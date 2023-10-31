using IdentityServer4.AspNetIdentity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class CustomProfileService : IProfileService
{
    private readonly UserManager<IdentityUser> _userManager;

    public CustomProfileService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subClaim = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(subClaim);
        var taskRoles = await _userManager.GetRolesAsync(user);
        var taskUserClaims = await _userManager.GetClaimsAsync(user);

        //await Task.WhenAll(taskRoles, taskUserClaims);

        if (user != null)
        {
            var claims = new List<Claim>();
            
            foreach(string role in taskRoles) 
            {
                claims.Add(new Claim("role", role));
            }
            foreach (Claim claim in taskUserClaims)
            {
                claims.Add(claim);
            }
            context.IssuedClaims.AddRange(claims);
        }
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        // Check if the user is active and set IsActive to true or false accordingly
        context.IsActive = true; // You can implement your own logic here.
        return Task.CompletedTask;
    }
}