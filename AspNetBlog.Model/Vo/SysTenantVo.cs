using AspNetBlog.Model.Tenants;
using SqlSugar;

namespace AspNetBlog.Model.Vo;

public class SysTenantVo
{
    public string Name { get; set; }
    public TenantTypeEnum TenantType { get; set; }
    public string ConfigId { get; set; }
    public string Host { get; set; }
    public DbType? DbType { get; set; }
    public string Connection { get; set; }
    public bool Status { get; set; } = true;
    public string Remark { get; set; }
}