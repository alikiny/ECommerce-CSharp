using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Services.PermissionRequirement
{
    public class PermissionRequirement : IAuthorizationRequirement { }

    public class ProductDeleteRequirementHandler : AuthorizationHandler<PermissionRequirement, Product>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement, Product resource)
        {
            if(context.User.IsInRole(Role.Admin.ToString()))
            {
                context.Succeed(requirement);
            } else if(context.User.FindFirstValue("NameIdentifier") == resource.SellerId.ToString())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}