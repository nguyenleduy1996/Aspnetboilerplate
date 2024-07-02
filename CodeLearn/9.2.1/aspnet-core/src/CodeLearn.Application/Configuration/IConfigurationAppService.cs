using System.Threading.Tasks;
using CodeLearn.Configuration.Dto;

namespace CodeLearn.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
