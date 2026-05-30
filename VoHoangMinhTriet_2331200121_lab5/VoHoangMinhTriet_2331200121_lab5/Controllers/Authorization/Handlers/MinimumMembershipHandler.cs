using Microsoft.AspNetCore.Authorization;
using VoHoangMinhTriet_2331200121_lab5.Controllers.Authorization.Requirements;

namespace VoHoangMinhTriet_2331200121_lab5.Controllers.Authorization.Handlers
{
    public class MinimumMembershipHandler : AuthorizationHandler<MinimumMembershipRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumMembershipRequirement requirement)
        {
            string? joinDateClaim = context.User.FindFirst(c => c.Type == "JoinDate")?.Value;
            if (joinDateClaim != null && DateTime.TryParse(joinDateClaim, out DateTime joinDate))
            {
                if (joinDate.AddDays(requirement.Days) <= DateTime.UtcNow)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
