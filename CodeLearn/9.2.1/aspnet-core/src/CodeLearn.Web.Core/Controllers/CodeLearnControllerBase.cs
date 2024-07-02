using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CodeLearn.Controllers
{
    public abstract class CodeLearnControllerBase: AbpController
    {
        protected CodeLearnControllerBase()
        {
            LocalizationSourceName = CodeLearnConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
