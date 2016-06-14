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
                .ForMember(e => e.NormalizedName, e => e.Ignore())
                .ForMember(e => e.ConcurrencyStamp, e => e.Ignore())
                .ForMember(e => e.Users, e => e.Ignore());
        }
    }
}
