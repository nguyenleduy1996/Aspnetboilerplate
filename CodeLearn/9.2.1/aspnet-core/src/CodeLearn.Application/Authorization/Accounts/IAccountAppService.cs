using System.Threading.Tasks;
using Abp.Application.Services;
using CodeLearn.Authorization.Accounts.Dto;

namespace CodeLearn.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
