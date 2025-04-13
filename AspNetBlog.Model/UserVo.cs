namespace AspNetBlog.Model;

// 用户视图模型
// 用来不给顶层 API 暴露数据库字段
public class UserVo
{
    public string? UserName { get; set; }
}