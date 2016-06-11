using Goofy.Application.Core;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Domain.Administration.Entity;
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

        public AdministrationConfigurator(RoleManager<GoofyRole> roleManager,
                                          UserManager<GoofyUser> userManager,
                                          IOptions<AdminConfiguration> adminConfigOptions)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
                foreach (var resource in SecurityServiceCollectionExtensions.Resources)
                {
                    foreach (var crudOperation in new[] { CrudOperation.Create, CrudOperation.Read, CrudOperation.Update, CrudOperation.Delete })
                    {
                        var claimName = SecurityUtils.GetPermissionName(resource, crudOperation);
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
