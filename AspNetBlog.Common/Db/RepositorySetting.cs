using AspNetBlog.Common.Core;
using AspNetBlog.Model.Tenants;
using SqlSugar;

namespace AspNetBlog.Common.Db;

public class RepositorySetting
{
    /// <summary>
    /// 配置租户
    /// </summary>
    public static void SetTenantEntityFilter(SqlSugarScopeProvider db)
    {
        if (App.User is not { ID: > 0, TenantId: > 0 })
        {
            return;
        }

        // 多租户 单表字段
        db.QueryFilter.AddTableFilter<ITenantEntity>(it => it.TenantId == App.User.TenantId || it.TenantId == 0);
    }
}