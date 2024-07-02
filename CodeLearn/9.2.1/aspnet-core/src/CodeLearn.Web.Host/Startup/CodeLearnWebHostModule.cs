using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CodeLearn.Configuration;

namespace CodeLearn.Web.Host.Startup
{
    [DependsOn(
       typeof(CodeLearnWebCoreModule))]
    public class CodeLearnWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CodeLearnWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeLearnWebHostModule).GetAssembly());
        }
    }
}
