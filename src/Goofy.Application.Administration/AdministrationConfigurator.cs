using Goofy.Application.Core.Abstractions;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Data;
using Goofy.Security;
using Goofy.Security.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Linq;
using System.Security.Claims;

namespace Goofy.Application.Administration
{
    public class AdministrationConfigurator : IRunAtStartup
    {
        private readonly AdminConfiguration _adminConfig;
        private readonly RoleManager<GoofyRole> _roleManager;
        private readonly UserManager<GoofyUser> _userManager;
        private readonly IAdministrationUnitOfWork _administrationContext;

        public AdministrationConfigurator(RoleManager<GoofyRole> roleManager,
                                          UserManager<GoofyUser> userManager,
                                          IAdministrationUnitOfWork administrationContext,
                                          IOptions<AdminConfiguration> adminConfigOptions)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _administrationContext = administrationContext;
            _adminConfig = adminConfigOptions.Value;
        }

        public int Order
        {
            get
            {
                return -800;
            }
        }

        public void Run()
        {
            //Check Administration Role
            CreateAdministrationRoleIfNotExist();

            //Check Administrator User
            CreateAdministorIfNotExist();

            //Crea Permissions
            CreatePermissionsIfNotExist();
        }

        private void CreatePermissionsIfNotExist()
        {
            var permissionsRepository = _administrationContext.Set<Permission>();
            var currentPermissions = permissionsRepository.GetAll().ToArray();

            foreach (var r in SecurityServiceCollectionExtensions.Resources)
            {
                foreach (var operation in r.Value)
                {
                    var permissionName = SecurityUtils.GetPermissionName(r.Key, operation);
                    if (currentPermissions.Where(p => p.Name == permissionName).Count() == 0)
                    {
                        permissionsRepository.Add(new Permission
                        {
                            Name = permissionName
                        });
                    }
                }
            }
            _administrationContext.SaveChanges();
        }

        private void CreateAdministorIfNotExist()
        {
            var administrators = _userManager.GetUsersInRoleAsync(_adminConfig.AdmininstrationRoleName).Result;
            if (administrators.Count == 0)
            {
                //An administrator User should be created

                var user = new GoofyUser
                {
                    UserName = _adminConfig.AdminUsername,
                    Email = _adminConfig.AdminEmail
                };
                var result = _userManager.CreateAsync(user, _adminConfig.AdminPassword).Result;
                if (!result.Succeeded)
                {
                    RaiseIdentityResultException(result);
                }
                //Add Administration Role to user
                result = _userManager.AddToRoleAsync(user, _adminConfig.AdmininstrationRoleName).Result;
                if (!result.Succeeded)
                {
                    RaiseIdentityResultException(result);
                }
            }
        }

        private static void RaiseIdentityResultException(IdentityResult result)
        {
            throw new Exception(result.Errors.First().Description);
        }

        private void CreateAdministrationRoleIfNotExist()
        {
            var exist = _roleManager.RoleExistsAsync(_adminConfig.AdmininstrationRoleName).Result;
            if (!exist)
            {
                //Create Administrator Role
                var role = new GoofyRole
                {
                    Name = _adminConfig.AdmininstrationRoleName,
                    Description = _adminConfig.AdministrationRoleDescription
                };
                var result = _roleManager.CreateAsync(role).Result;
                if (!result.Succeeded)
                {
                    RaiseIdentityResultException(result);
                }
                foreach (var r in SecurityServiceCollectionExtensions.Resources)
                {
                    foreach (var crudOperation in r.Value)
                    {
                        var claimName = SecurityUtils.GetPermissionName(r.Key, crudOperation);
                        result = _roleManager.AddClaimAsync(role, new Claim(claimName, "")).Result;
                        if (!result.Succeeded)
                        {
                            RaiseIdentityResultException(result);
                        }
                    }
                }
            }
        }
    }
}
