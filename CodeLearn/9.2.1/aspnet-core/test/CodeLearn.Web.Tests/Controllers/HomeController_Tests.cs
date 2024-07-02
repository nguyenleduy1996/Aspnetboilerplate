using System.Threading.Tasks;
using CodeLearn.Models.TokenAuth;
using CodeLearn.Web.Controllers;
using Shouldly;
using Xunit;

namespace CodeLearn.Web.Tests.Controllers
{
    public class HomeController_Tests: CodeLearnWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}