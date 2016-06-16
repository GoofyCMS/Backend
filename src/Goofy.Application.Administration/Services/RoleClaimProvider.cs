using System;
using System.Collections.Generic;
using System.Security.Claims;
using Goofy.Security.Services.Abstractions;
using Goofy.Domain.Administration.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace Goofy.Application.Administration.Services
{
    public class RoleClaimProvider : IRoleClaimProvider
    {
        private readonly RoleManager<GoofyRole> _roleManager;

        public RoleClaimProvider(RoleManager<GoofyRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IEnumerable<Claim> RoleClaims(IEnumerable<string> roles)
        {
            return _roleManager.Roles
                .Where(r => roles.Contains(r.Name))
                .SelectMany(r => r.Claims)
                .Select(r => new { r.ClaimType, r.ClaimValue })
                .ToArray().Select(r => new Claim(r.ClaimType, r.ClaimValue));
        }

    }
}
