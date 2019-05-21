using System.Threading.Tasks;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PersonalWebsite.Authorization
{
  public class ProjectAdministratorsAuthorizationHandler
    : AuthorizationHandler<OperationAuthorizationRequirement, Project>
  {
    protected override Task HandleRequirementAsync(
                                          AuthorizationHandlerContext context,
                                OperationAuthorizationRequirement requirement,
                                 Project resource)
    {
      if (context.User == null)
      {
        return Task.CompletedTask;
      }

      // Administrators can do anything.
      if (context.User.IsInRole(Developers.ProjectAdministratorsRole))
      {
        context.Succeed(requirement);
      }
      return Task.CompletedTask;
    }
  }
}
