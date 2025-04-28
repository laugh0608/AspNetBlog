using AspNetBlog.Model.Tenants;
using SqlSugar;

namespace AspNetBlog.Model;

/// <summary>
/// 系统租户表
/// 根据 TenantType 分为两种方案：
/// 1.按租户字段区分
/// 2.按租户分库
/// 注意：使用租户 Id 方案，无需配置分库的连接
/// </summary>
public class SysTenant : RootEntityTKey<long>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 租户类型
    /// </summary>
    public TenantTypeEnum TenantType { get; set; }

    /// <summary>
    /// 数据库 /租户标识，不可重复
    /// 使用 Id 方案，可无需配置
    /// </summary>
    [SugarColumn(Length = 64)]
    public string ConfigId { get; set; }

    /// <summary>
    /// 主机
    /// 使用 Id 方案，可无需配置
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string Host { get; set; }

    /// <summary>
    /// 数据库类型
    /// 使用 Id 方案，可无需配置
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DbType? DbType { get; set; }

    /// <summary>
    /// 数据库连接
    /// 使用 Id 方案，可无需配置
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string Connection { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public bool Status { get; set; } = true;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string Remark { get; set; }
}