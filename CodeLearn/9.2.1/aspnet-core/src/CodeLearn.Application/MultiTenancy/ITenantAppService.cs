using Abp.Application.Services;
using CodeLearn.MultiTenancy.Dto;

namespace CodeLearn.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

