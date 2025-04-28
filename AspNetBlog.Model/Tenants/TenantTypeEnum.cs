using System.ComponentModel;

namespace AspNetBlog.Model.Tenants;

/// <summary>
/// 租户隔离方案
/// </summary>
public enum TenantTypeEnum
{
    // 不隔离
    None = 0,

    /// <summary>
    /// Id 隔离，分字段
    /// </summary>
    [Description("Id 隔离")]
    Id = 1,
    
    /// <summary>
    /// 库隔离，分数据库（多库）
    /// </summary>
    [Description("库隔离")]
    Db = 2,

    /// <summary>
    /// 表隔离，分表
    /// </summary>
    [Description("表隔离")]
    Tables = 3,
}