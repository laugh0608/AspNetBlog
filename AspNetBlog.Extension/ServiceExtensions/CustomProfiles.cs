using AspNetBlog.Model;
using AutoMapper;

namespace AspNetBlog.Extension.ServiceExtensions;

public class CustomProfiles : Profile
{
    // 配置构造函数，用来创建关系映射
    public CustomProfiles()
    {
        CreateMap<Role, RoleVo>()
            .ForMember(a => a.RoleName, o => o.MapFrom(d => d.Name));
        CreateMap<RoleVo, Role>()
            .ForMember(a => a.Name, o => o.MapFrom(d => d.RoleName));
    }
}