using System.Threading.Tasks;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace PersonalWebsite.Authorization
{
  public class ProjectIsOwnerAuthorizationHandler
               : AuthorizationHandler<OperationAuthorizationRequirement, Project>
  {
    UserManager<IdentityUser> _userManager;

    public ProjectIsOwnerAuthorizationHandler(UserManager<IdentityUser>
        userManager)
    {
      _userManager = userManager;
    }

    protected override Task
        HandleRequirementAsync(AuthorizationHandlerContext context,
                               OperationAuthorizationRequirement requirement,
                               Project resource)
    {
      if (context.User == null || resource == null)
      {
        // Return Task.FromResult(0) if targeting a version of
        // .NET Framework older than 4.6:
        return Task.CompletedTask;
      }

      // If we're not asking for CUD permission, return.

      if (requirement.Name == Commenters.ReadOperationName)
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }
}
