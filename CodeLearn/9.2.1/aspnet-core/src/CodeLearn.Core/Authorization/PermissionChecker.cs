using Abp.Authorization;
using CodeLearn.Authorization.Roles;
using CodeLearn.Authorization.Users;

namespace CodeLearn.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
