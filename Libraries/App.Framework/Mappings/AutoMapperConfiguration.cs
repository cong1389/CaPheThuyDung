using AutoMapper;

namespace App.Framework.Mappings
{
    public class AutoMapperConfiguration
	{
	    public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
                cfg.AddProfile<ViewModelToDomainMappingProfile>();
            });
		}
	}
}