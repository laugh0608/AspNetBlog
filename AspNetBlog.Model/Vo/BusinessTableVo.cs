namespace AspNetBlog.Model.Vo;

/// <summary>
/// 业务数据视图
/// </summary>
public class BusinessTableVo
{
    public long TenantId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
}