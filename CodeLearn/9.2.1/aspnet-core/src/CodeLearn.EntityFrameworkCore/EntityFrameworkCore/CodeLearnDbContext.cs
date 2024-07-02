using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CodeLearn.Authorization.Roles;
using CodeLearn.Authorization.Users;
using CodeLearn.MultiTenancy;

namespace CodeLearn.EntityFrameworkCore
{
    public class CodeLearnDbContext : AbpZeroDbContext<Tenant, Role, User, CodeLearnDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public CodeLearnDbContext(DbContextOptions<CodeLearnDbContext> options)
            : base(options)
        {
        }
    }
}
