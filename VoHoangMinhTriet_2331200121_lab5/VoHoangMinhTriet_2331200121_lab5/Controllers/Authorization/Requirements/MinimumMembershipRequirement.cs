using Microsoft.AspNetCore.Authorization;

namespace VoHoangMinhTriet_2331200121_lab5.Controllers.Authorization.Requirements
{
    public class MinimumMembershipRequirement : IAuthorizationRequirement
    {
        public int Days { get; set; }
        public MinimumMembershipRequirement(int days)
        {
            Days = days;
        }
    }
}
