using Microsoft.AspNetCore.Authorization;

namespace AspNetBlog.Extension.ServiceExtensions;

// public class PermissionRequirement : AuthorizationHandler<PermissionRequirement>, IAuthorizationRequirement
// {
//     protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
//     {
//         await Task.CompletedTask;
//         context.Succeed(requirement);
//         return;
//     }
// }

public class PermissionRequirement : IAuthorizationRequirement
{
    public List<PermissionItem> Permissions { get; set; } = new List<PermissionItem>();
}