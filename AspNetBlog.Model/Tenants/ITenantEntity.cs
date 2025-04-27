using SqlSugar;

namespace AspNetBlog.Model.Tenants;

/// <summary>
/// 租户模型接口
/// </summary>
public interface ITenantEntity
{
    /// <summary>
    /// 租户 Id
    /// </summary>
    [SugarColumn(DefaultValue = "0")]
    public long TenantId { get; set; }
}