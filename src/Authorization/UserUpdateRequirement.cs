using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Authorization
{
    public class UserUpdateRequirement : IAuthorizationRequirement { }

    public class UserUpdateRequirementHandler : AuthorizationHandler<UserUpdateRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserUpdateRequirement requirement, User resource)
        {
            if(context.User.IsInRole(Role.Admin.ToString()))
            {
                context.Succeed(requirement);
            } else if(context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.ID.ToString())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
