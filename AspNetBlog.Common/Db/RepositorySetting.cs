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
        // 这里的条件要根据实际业务逻辑来，TenantId 是否必须要大于 0，如果说允许等于 0，那么这里的 return 就要格外注意
        // 因为这里的 return 是直接返回表里所有的数据，而不执行 AddTableFilter<ITenantEntity>() 过滤
        if (App.User is not { ID: > 0, TenantId: > 0 })
        {
            return;
        }

        // 多租户 单表字段
        db.QueryFilter.AddTableFilter<ITenantEntity>(it => it.TenantId == App.User.TenantId || it.TenantId == 0);
    }
}