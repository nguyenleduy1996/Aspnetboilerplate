using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CodeLearn.EntityFrameworkCore;
using CodeLearn.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace CodeLearn.Web.Tests
{
    [DependsOn(
        typeof(CodeLearnWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class CodeLearnWebTestModule : AbpModule
    {
        public CodeLearnWebTestModule(CodeLearnEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeLearnWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(CodeLearnWebMvcModule).Assembly);
        }
    }
}