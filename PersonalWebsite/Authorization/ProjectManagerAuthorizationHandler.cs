using System.Threading.Tasks;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace PersonalWebsite.Authorization
{
  public class ProjectManagerAuthorizationHandler :
      AuthorizationHandler<OperationAuthorizationRequirement, Project>
  {
    protected override Task
        HandleRequirementAsync(AuthorizationHandlerContext context,
                               OperationAuthorizationRequirement requirement,
                               Project resource)
    {
      if (context.User == null || resource == null)
      {
        return Task.CompletedTask;
      }

      // If not asking for create/update, return.
      if (requirement.Name != Developers.CreateOperationName &&
          requirement.Name != Developers.UpdateOperationName)
      {
        return Task.CompletedTask;
      }

      // Managers can create or update.
      if (context.User.IsInRole(Developers.ProjectManagersRole))
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}
