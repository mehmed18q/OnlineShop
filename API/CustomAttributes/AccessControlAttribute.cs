using Application.Interfaces;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.CustomAttributes
{
    public class AccessControlAttribute : ActionFilterAttribute
    {
        public required string Permission { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Guid? userId = context.HttpContext.User.GetClaim<Guid?>(IdentityUtilities.UserIdClaimIdentity);
            if (userId is not null)
            {
                IPermissionService? permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();
                if (permissionService is not null)
                {
                    if (await permissionService.CheckPermission(userId.Value, Permission))
                    {
                        await base.OnActionExecutionAsync(context, next);
                    }
                }
            }

            context.Result = new BadRequestObjectResult("Access Denied!");
        }
    }
}
