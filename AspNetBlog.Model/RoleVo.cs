namespace AspNetBlog.Model;

// 角色视图模型
// 用来不给顶层 API 暴露数据库字段
public class RoleVo
{
    public string? RoleName { get; set; }
}