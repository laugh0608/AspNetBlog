using System.ComponentModel;

namespace AspNetBlog.Model.Tenants;

/// <summary>
/// 租户隔离方案
/// </summary>
public enum TenantTypeEnum
{
    None = 0,

    /// <summary>
    /// Id 隔离
    /// </summary>
    [Description("Id 隔离")]
    Id = 1,

    /// <summary>
    /// 表隔离
    /// </summary>
    [Description("表隔离")]
    Tables = 3,
}