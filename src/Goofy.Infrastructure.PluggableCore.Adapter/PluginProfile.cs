using AutoMapper;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Application.PluggableCore.DTO;

namespace Goofy.Infrastructure.Plugins.Adapter
{
    public class PluginProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Plugin, PluginItem>();
            CreateMap<PluginItem, Plugin>();
        }
    }
}
