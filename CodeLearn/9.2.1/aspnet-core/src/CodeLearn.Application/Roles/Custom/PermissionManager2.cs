using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Localization;
using AutoMapper.Internal.Mappers;
using CodeLearn.Authorization.Roles;
using CodeLearn.Authorization;
using CodeLearn.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLearn.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.IdentityFramework;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Features;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp;
using System.Collections.Immutable;

namespace CodeLearn.Roles.Custom
{
    public class PermissionManager2 : PermissionDefinitionContextBase, IPermissionManager, ISingletonDependency
    {
        private readonly IIocManager _iocManager;

        private readonly IAuthorizationConfiguration _authorizationConfiguration;

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IMultiTenancyConfig _multiTenancy;

        public IAbpSession AbpSession { get; set; }

        //
        // Summary:
        //     Constructor.
        public PermissionManager2(IIocManager iocManager, IAuthorizationConfiguration authorizationConfiguration, IUnitOfWorkManager unitOfWorkManager, IMultiTenancyConfig multiTenancy)
        {
            _iocManager = iocManager;
            _authorizationConfiguration = authorizationConfiguration;
            _unitOfWorkManager = unitOfWorkManager;
            _multiTenancy = multiTenancy;
            AbpSession = NullAbpSession.Instance;
        }

        public virtual void Initialize()
        {
            foreach (Type provider in _authorizationConfiguration.Providers)
            {
                using IDisposableDependencyObjectWrapper<AuthorizationProvider> disposableDependencyObjectWrapper = _iocManager.ResolveAsDisposable<AuthorizationProvider>(provider);
                disposableDependencyObjectWrapper.Object.SetPermissions(this);
            }

            Permissions.AddAllPermissions();
        }

        public virtual Permission GetPermission(string name)
        {
            return Permissions.GetOrDefault(name) ?? throw new AbpException("There is no permission with name: " + name);
        }

        public virtual IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true)
        {
            using IDisposableDependencyObjectWrapper<FeatureDependencyContext> disposableDependencyObjectWrapper = _iocManager.ResolveAsDisposable<FeatureDependencyContext>();
            FeatureDependencyContext featureDependencyContextObject = disposableDependencyObjectWrapper.Object;
            featureDependencyContextObject.TenantId = GetCurrentTenantId();
            return (from p in Permissions.Values.WhereIf(tenancyFilter, (Permission p) => p.MultiTenancySides.HasFlag(GetCurrentMultiTenancySide()))
                    where p.FeatureDependency == null || GetCurrentMultiTenancySide() == MultiTenancySides.Host || p.FeatureDependency.IsSatisfied(featureDependencyContextObject)
                    select p).ToImmutableList();
        }

        public virtual async Task<IReadOnlyList<Permission>> GetAllPermissionsAsync(bool tenancyFilter = true)
        {
            using IDisposableDependencyObjectWrapper<FeatureDependencyContext> featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>();
            FeatureDependencyContext @object = featureDependencyContext.Object;
            @object.TenantId = GetCurrentTenantId();
            List<Permission> unfilteredPermissions = (from p in Permissions.Values.WhereIf(tenancyFilter, (Permission p) => p.MultiTenancySides.HasFlag(GetCurrentMultiTenancySide()))
                                                      where p.FeatureDependency == null || GetCurrentMultiTenancySide() == MultiTenancySides.Host
                                                      select p).ToList();
            return (await FilterSatisfiedPermissionsAsync(@object, unfilteredPermissions).ConfigureAwait(continueOnCapturedContext: false)).ToImmutableList();
        }

        public virtual IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides)
        {
            using IDisposableDependencyObjectWrapper<FeatureDependencyContext> disposableDependencyObjectWrapper = _iocManager.ResolveAsDisposable<FeatureDependencyContext>();
            FeatureDependencyContext featureDependencyContextObject = disposableDependencyObjectWrapper.Object;
            featureDependencyContextObject.TenantId = GetCurrentTenantId();
            return (from p in Permissions.Values
                    where p.MultiTenancySides.HasFlag(multiTenancySides)
                    where p.FeatureDependency == null || GetCurrentMultiTenancySide() == MultiTenancySides.Host || (p.MultiTenancySides.HasFlag(MultiTenancySides.Host) && multiTenancySides.HasFlag(MultiTenancySides.Host)) || p.FeatureDependency.IsSatisfied(featureDependencyContextObject)
                    select p).ToImmutableList();
        }

        public virtual async Task<IReadOnlyList<Permission>> GetAllPermissionsAsync(MultiTenancySides multiTenancySides)
        {
            using IDisposableDependencyObjectWrapper<FeatureDependencyContext> featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>();
            FeatureDependencyContext @object = featureDependencyContext.Object;
            @object.TenantId = GetCurrentTenantId();
            List<Permission> unfilteredPermissions = (from p in Permissions.Values
                                                      where p.MultiTenancySides.HasFlag(multiTenancySides)
                                                      where p.FeatureDependency == null || GetCurrentMultiTenancySide() == MultiTenancySides.Host || (p.MultiTenancySides.HasFlag(MultiTenancySides.Host) && multiTenancySides.HasFlag(MultiTenancySides.Host))
                                                      select p).ToList();
            return (await FilterSatisfiedPermissionsAsync(@object, unfilteredPermissions).ConfigureAwait(continueOnCapturedContext: false)).ToImmutableList();
        }

        private async Task<IList<Permission>> FilterSatisfiedPermissionsAsync(FeatureDependencyContext featureDependencyContextObject, IList<Permission> unfilteredPermissions)
        {
            List<Permission> filteredPermissions = new List<Permission>();
            for (int i = 0; i < unfilteredPermissions.Count; i++)
            {
                Permission permission = unfilteredPermissions[i];
                bool flag = permission.FeatureDependency != null;
                if (flag)
                {
                    flag = !(await permission.FeatureDependency.IsSatisfiedAsync(featureDependencyContextObject).ConfigureAwait(continueOnCapturedContext: false));
                }

                if (!flag)
                {
                    filteredPermissions.Add(permission);
                }
            }

            return filteredPermissions;
        }

        private MultiTenancySides GetCurrentMultiTenancySide()
        {
            if (_unitOfWorkManager.Current != null)
            {
                if (!_multiTenancy.IsEnabled || _unitOfWorkManager.Current.GetTenantId().HasValue)
                {
                    return MultiTenancySides.Tenant;
                }

                return MultiTenancySides.Host;
            }

            return AbpSession.MultiTenancySide;
        }

        private int? GetCurrentTenantId()
        {
            if (_unitOfWorkManager.Current != null)
            {
                return _unitOfWorkManager.Current.GetTenantId();
            }

            return AbpSession.TenantId;
        }
    }
}
