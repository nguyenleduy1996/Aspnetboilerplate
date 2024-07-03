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
using LearnAPI.Repos;

namespace CodeLearn.Controllers
{

    [AbpAuthorize(PermissionNames.Pages_Roles)]
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
        private readonly LearndataContext _context;

        public TokenAuth2Controller(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration, RoleManager roleManager, UserManager userManager, PermissionManager2 permissionManager, LearndataContext context
            )
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration; _roleManager = roleManager;
            _userManager = userManager;
            _permissionManager = permissionManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult  Gettest()
        {
            var user = _userManager.Users.FirstOrDefault();
            var result = _context.TblMenus.ToList();
            return Ok(result);
        }
    }
}
