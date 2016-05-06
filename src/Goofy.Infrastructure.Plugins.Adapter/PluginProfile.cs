using AutoMapper;
using Goofy.Domain.Plugins.Entity;
using Goofy.Application.Plugins.DTO;

namespace Goofy.Infrastructure.Plugins.Adapter
{
    public class PluginProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Plugin, PluginItem>();
        }
    }
}
