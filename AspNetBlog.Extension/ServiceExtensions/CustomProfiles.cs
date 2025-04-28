using AspNetBlog.Model;
using AspNetBlog.Model.Tenants;
using AspNetBlog.Model.Vo;
using AutoMapper;

namespace AspNetBlog.Extension.ServiceExtensions;

public class CustomProfiles : Profile
{
    /// <summary>
    /// 配置构造函数，用来创建关系映射
    /// </summary>
    public CustomProfiles()
    {
        // 角色模型的关系映射
        CreateMap<Role, RoleVo>()
            .ForMember(a => a.RoleName, o => o.MapFrom(d => d.Name));
        CreateMap<RoleVo, Role>()
            .ForMember(a => a.Name, o => o.MapFrom(d => d.RoleName));

        // 用户模型的关系映射
        CreateMap<SysUserInfo, UserVo>()
            .ForMember(a => a.UserName, o => o.MapFrom(d => d.Name));
        CreateMap<UserVo, SysUserInfo>()
            .ForMember(a => a.Name, o => o.MapFrom(d => d.UserName));
        
        // 日志数据库的关系映射
        CreateMap<AuditSqlLog, AuditSqlLogVo>();
        CreateMap<AuditSqlLogVo, AuditSqlLog>();
        
        // 多租户单表字段的关系映射
        CreateMap<BusinessTable, BusinessTableVo>();
        CreateMap<BusinessTableVo, BusinessTable>();
        
        // 多租户分表的关系映射
        CreateMap<MultiBusinessTable, MultiBusinessTableVo>();
        CreateMap<MultiBusinessTableVo, MultiBusinessTable>();
        
        // 多租户分库的关系映射
        CreateMap<SubLibraryBusinessTable, SubLibraryBusinessTableVo>();
        CreateMap<SubLibraryBusinessTableVo, SubLibraryBusinessTable>();
    }
}