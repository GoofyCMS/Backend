using AutoMapper;
using Goofy.Domain.Administration.Entity;
using Goofy.Application.Administration.DTO;
using Goofy.Domain.Identity.Entity;

namespace Goofy.Infrastructure.Administration.Adapter
{
    public class AdministrationProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Plugin, PluginItem>();
            CreateMap<PluginItem, Plugin>();

            CreateMap<IdentityRoleClaim, IdentityRoleClaimItem>();
            CreateMap<IdentityRoleClaimItem, IdentityRoleClaim>();

            CreateMap<GoofyRole, GoofyRoleItem>()
                .ForMember(e => e.Claims, e => e.Ignore());
            CreateMap<GoofyRoleItem, GoofyRole>()
                .ForMember(e => e.Claims, e => e.Ignore())
                .ForMember(e => e.NormalizedName, e => e.Ignore())
                .ForMember(e => e.ConcurrencyStamp, e => e.Ignore())
                .ForMember(e => e.Users, e => e.Ignore());

            CreateMap<GoofyUserItem, GoofyUser>()
                .ForMember(e => e.Claims, e => e.Ignore())
                .ForMember(e => e.Roles, e => e.Ignore())
                .ForMember(e => e.NormalizedUserName, e => e.Ignore())
                .ForMember(e => e.NormalizedEmail, e => e.Ignore())
                .ForMember(e => e.Email, e => e.Ignore())
                .ForMember(e => e.EmailConfirmed, e => e.Ignore())
                .ForMember(e => e.PasswordHash, e => e.Ignore())
                .ForMember(e => e.SecurityStamp, e => e.Ignore())
                .ForMember(e => e.ConcurrencyStamp, e => e.Ignore())
                .ForMember(e => e.PhoneNumber, e => e.Ignore())
                .ForMember(e => e.PhoneNumberConfirmed, e => e.Ignore())
                .ForMember(e => e.TwoFactorEnabled, e => e.Ignore())
                .ForMember(e => e.LockoutEnd, e => e.Ignore())
                .ForMember(e => e.LockoutEnabled, e => e.Ignore())
                .ForMember(e => e.AccessFailedCount, e => e.Ignore())
                .ForMember(e => e.Logins, e => e.Ignore());
            CreateMap<GoofyUser, GoofyUserItem>()
                .ForMember(e => e.Claims, e => e.Ignore())
                .ForMember(e => e.Roles, e => e.Ignore());

            CreateMap<IdentityUserRoleItem, IdentityUserRole>();
            CreateMap<IdentityUserRole, IdentityUserRoleItem>()
                .ForMember(e => e.GoofyUser, e => e.Ignore());

            CreateMap<IdentityUserClaimItem, IdentityUserClaim>();
            CreateMap<IdentityUserClaim, IdentityUserClaimItem>();

            CreateMap<Permission, PermissionItem>();
            CreateMap<PermissionItem, Permission>();
        }
    }
}
