using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Authorization
{
    public class ProductDeleteRequirement : IAuthorizationRequirement { }

    public class ProductDeleteRequirementHandler : AuthorizationHandler<ProductDeleteRequirement, Product>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProductDeleteRequirement requirement, Product resource)
        {
            if(context.User.IsInRole(Role.Admin.ToString()))
            {
                context.Succeed(requirement);
            } else if(context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.SellerId.ToString())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
