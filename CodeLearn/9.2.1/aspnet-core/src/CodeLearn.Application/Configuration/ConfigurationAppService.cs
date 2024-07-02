using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using CodeLearn.Configuration.Dto;

namespace CodeLearn.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CodeLearnAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
