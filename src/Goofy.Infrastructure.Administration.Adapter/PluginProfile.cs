using AutoMapper;
using Goofy.Domain.Administration.Entity;
using Goofy.Application.Administration.DTO;


namespace Goofy.Infrastructure.Administration.Adapter
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
