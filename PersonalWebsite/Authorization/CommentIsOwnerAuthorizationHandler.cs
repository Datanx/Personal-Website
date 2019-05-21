using System.Threading.Tasks;
using PersonalWebsite.Data;
using PersonalWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace PersonalWebsite.Authorization
{
    public class CommentIsOwnerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Comment>
    {
        UserManager<IdentityUser> _userManager;

        public CommentIsOwnerAuthorizationHandler(UserManager<IdentityUser> 
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Comment resource)
        {
            if (context.User == null || resource == null)
            {
                // Return Task.FromResult(0) if targeting a version of
                // .NET Framework older than 4.6:
                return Task.CompletedTask;
            }

            // If we're not asking for CRUD permission, return.

            if (requirement.Name != Commenters.CreateOperationName &&
                requirement.Name != Commenters.ReadOperationName   &&
                requirement.Name != Commenters.UpdateOperationName &&
                requirement.Name != Commenters.DeleteOperationName )
            {
                return Task.CompletedTask;
            }

            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
