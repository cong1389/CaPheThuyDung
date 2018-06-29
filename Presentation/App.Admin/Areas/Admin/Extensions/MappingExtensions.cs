using App.Core.Plugins;
using App.FakeEntity.Plugins;
using AutoMapper;

namespace App.Admin.Areas.Admin.Extensions
{
	public static class MappingExtensions
    {
        public static PluginModel ToModel(this PluginDescriptor entity)
        {
            return Mapper.Map<PluginDescriptor, PluginModel>(entity);
        }
    }
}