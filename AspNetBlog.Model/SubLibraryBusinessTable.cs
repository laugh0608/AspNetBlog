using AspNetBlog.Model.Tenants;

namespace AspNetBlog.Model;

/// <summary>
/// 多租户 - 多库方案，业务表
/// 公共库无需标记 [MultiTenant] 特性
/// </summary>
[MultiTenant(TenantTypeEnum.Db)]
public class SubLibraryBusinessTable : RootEntityTKey<long>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    public decimal Amount { get; set; }
}