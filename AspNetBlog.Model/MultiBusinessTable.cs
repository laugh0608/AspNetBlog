using AspNetBlog.Model.Tenants;

namespace AspNetBlog.Model;

/// <summary>
/// 多租户-多表方案 业务表 <br/>
/// </summary>
[MultiTenant(TenantTypeEnum.Tables)]    // 多租户模式，分表
public class MultiBusinessTable : RootEntityTKey<long>
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