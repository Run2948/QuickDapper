using AutoMapper;
using AutoMapper.Configuration;
using Quick.Repository.AutoMapper;
using Quick.Services.AutoMapper;
using QuickWeb.ViewModels.AutoMapper;

namespace QuickWeb
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            var configuration = new MapperConfigurationExpression();

            configuration.AddProfile<RepositoryMappingProfile>();
            configuration.AddProfile<ServiceMappingProfile>();
            configuration.AddProfile<ViewModelMappingProfile>();

            Mapper.Initialize(configuration);
            // only during development, validate your mappings; remove it before release 
            Mapper.AssertConfigurationIsValid();
        }

        // 使用案例：
        //    var result = Mapper.Map<List<User>, List<UserDto>>(data);
    }
}