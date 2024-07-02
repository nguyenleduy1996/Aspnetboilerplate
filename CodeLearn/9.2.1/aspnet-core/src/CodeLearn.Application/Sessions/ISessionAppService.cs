using System.Threading.Tasks;
using Abp.Application.Services;
using CodeLearn.Sessions.Dto;

namespace CodeLearn.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
