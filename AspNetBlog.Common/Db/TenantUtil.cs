using System.Reflection;
using AspNetBlog.Model;
using AspNetBlog.Model.Tenants;
using SqlSugar;

namespace AspNetBlog.Common.Db;

public static class TenantUtil
{
    public static List<Type> GetTenantEntityTypes(TenantTypeEnum? tenantType = null)
    {
        return RepositorySetting.Entitys
            .Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass)
            .Where(s => IsTenantEntity(s, tenantType))
            .ToList();
    }

    public static bool IsTenantEntity(this Type u, TenantTypeEnum? tenantType = null)
    {
        var mta = u.GetCustomAttribute<MultiTenantAttribute>();
        if (mta is null)
        {
            return false;
        }

        if (tenantType != null)
        {
            if (mta.TenantType != tenantType)
            {
                return false;
            }
        }

        return true;
    }

    public static string GetTenantTableName(this Type type, ISqlSugarClient db, string id)
    {
        var entityInfo = db.EntityMaintenance.GetEntityInfo(type);
        // 多租户分表的核心逻辑：
        return $@"{entityInfo.DbTableName}_{id}";
    }

    public static void SetTenantTable(this ISqlSugarClient db, string id)
    {
        var types = GetTenantEntityTypes(TenantTypeEnum.Tables);

        foreach (var type in types)
        {
            db.MappingTables.Add(type.Name, type.GetTenantTableName(db, id));
        }
    }
    
    // 多租户 - 多库
    public static ConnectionConfig GetConnectionConfig(this SysTenant tenant)
    {
        if (tenant.DbType is null)
        {
            throw new ArgumentException("Tenant DbType Must");
        }

        return new ConnectionConfig()
        {
            ConfigId = tenant.ConfigId,
            DbType = tenant.DbType.Value,
            ConnectionString = tenant.Connection,
            IsAutoCloseConnection = true,
            MoreSettings = new ConnMoreSettings()
            {
                IsAutoRemoveDataCache = true,
                SqlServerCodeFirstNvarchar = true,
            },
        };
    }
}