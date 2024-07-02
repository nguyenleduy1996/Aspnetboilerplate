using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using CodeLearn.Authentication.JwtBearer;
using CodeLearn.Authorization;
using CodeLearn.Authorization.Users;
using CodeLearn.Models.TokenAuth;
using CodeLearn.MultiTenancy;
using Abp.Domain.Repositories;
using CodeLearn.Authorization.Roles;
using CodeLearn.Roles.Custom;

namespace CodeLearn.Controllers
{


    [Route("api/[controller]/[action]")]
    public class TokenAuth2Controller : CodeLearnControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;

        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly PermissionManager2 _permissionManager;

   /*     public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager, PermissionManager2 permissionManager)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _permissionManager = permissionManager;
        }*/



        public TokenAuth2Controller(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration, RoleManager roleManager, UserManager userManager, PermissionManager2 permissionManager
            )
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration; _roleManager = roleManager;
            _userManager = userManager;
            _permissionManager = permissionManager;
        }
        [HttpGet]
        public IActionResult  Gettest()
        {
            var user = _userManager.Users.FirstOrDefault();
            var user2 = _permissionManager.GetAllPermissions();
            return Ok(user);
        }
    }
}
