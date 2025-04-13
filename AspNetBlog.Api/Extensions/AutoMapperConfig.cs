using AutoMapper;

namespace AspNetBlog.Api.Extensions;

// 静态全局 AutoMapper 配置文件
public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CustomProfiles());
        });
    }
}