using BusinessObjects.Enums;
using Microsoft.AspNetCore.Authorization;

namespace SelfDrivingCarRentalPlatform.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeRoleAttribute(params UserRole[] roles)
        {
            this.Roles = string.Join(",", roles.Select(role => role.ToString()));
        }
    }
}
