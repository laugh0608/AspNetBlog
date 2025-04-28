using System.Linq.Expressions;
using System.Reflection;
using AspNetBlog.Common.Core;
using AspNetBlog.Common.Db;
using AspNetBlog.Model;
using AspNetBlog.Model.Tenants;
using AspNetBlog.Repository.UnitOfWork;
using SqlSugar;

namespace AspNetBlog.Repository.Base;

// 继承 IBaseRepository 基类
// 目的是通过传递不同的实体对象，将数据库查询到的内容进行不同的转化和推送
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
{
    // public async Task<List<TEntity>> Query()
    // {
    //     // 手动定义一个 await Task
    //     await Task.CompletedTask;
    //     // 手动传入一个数据
    //     var data = "[{\"Id\": 18,\"Name\":\"nameName\"}]";
    //     // 并序列化
    //     return JsonConvert.DeserializeObject<List<TEntity>>(data) ?? new List<TEntity>();
    // }
    
    // 使用数据库
    private readonly SqlSugarScope _dbBase;
    private readonly IUnitOfWorkManage _unitOfWorkManage;
    public ISqlSugarClient Db => _db;
    
    // public BaseRepository(ISqlSugarClient sqlSugarClient)
    // {
    //     _dbBase = sqlSugarClient;
    // }
    // 更改切换数据库的逻辑
    private ISqlSugarClient _db
    {
        get
        {
            ISqlSugarClient db = _dbBase;

            // 修改使用 model 备注字段作为切换数据库条件，使用 sqlsugar TenantAttribute 存放数据库 ConnId
            // 参考 https://www.donet5.com/Home/Doc?typeId=2246
            var tenantAttr = typeof(TEntity).GetCustomAttribute<TenantAttribute>();
            if (tenantAttr != null)
            {
                // 统一处理 configId 小写
                db = _dbBase.GetConnectionScope(tenantAttr.configId.ToString().ToLower());
                return db;
            }
            
            // 多租户实现
            var mta = typeof(TEntity).GetCustomAttribute<MultiTenantAttribute>();
            if (mta is { TenantType: TenantTypeEnum.Db })
            {
                // 获取租户信息 租户信息可以提前缓存下来 
                if (App.User is { TenantId: > 0 })
                {
                    //.WithCache()
                    var tenant = db.Queryable<SysTenant>().Where(s => s.Id == App.User.TenantId).First();
                    if (tenant != null)
                    {
                        var iTenant = db.AsTenant();
                        if (!iTenant.IsAnyConnection(tenant.ConfigId))
                        {
                            iTenant.AddConnection(tenant.GetConnectionConfig());
                        }

                        return iTenant.GetConnectionScope(tenant.ConfigId);
                    }
                }
            }
            
            return db;
        }
    }
    public BaseRepository(IUnitOfWorkManage unitOfWorkManage)
    {
        _unitOfWorkManage = unitOfWorkManage;
        _dbBase = unitOfWorkManage.GetDbClient();
    }

    public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression = null)
    {
        await Console.Out.WriteLineAsync(Db.GetHashCode().ToString());
        return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
    }
    public async Task<List<TEntity>> QueryWithCache(Expression<Func<TEntity, bool>> whereExpression = null)
    {
        return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).WithCache().ToListAsync();
    }
    
    /// <summary>
    /// 向数据库中写入实体数据
    /// </summary>
    /// <param name="entity">博文实体类</param>
    /// <returns></returns>
    public async Task<long> Add(TEntity entity)
    {
        var insert = _db.Insertable(entity);
        return await insert.ExecuteReturnSnowflakeIdAsync();
    }
    
    /// <summary>
    /// 分表查询
    /// </summary>
    /// <param name="whereExpression">条件表达式</param>
    /// <param name="orderByFields">排序字段，如 name asc,age desc</param>
    /// <returns></returns>
    public async Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression,
        string orderByFields = null)
    {
        return await _db.Queryable<TEntity>()
            .SplitTable()
            .OrderByIF(!string.IsNullOrEmpty(orderByFields), orderByFields)
            .WhereIF(whereExpression != null, whereExpression)
            .ToListAsync();
    }

    /// <summary>
    /// 写入实体数据
    /// </summary>
    /// <param name="entity">数据实体</param>
    /// <returns></returns>
    public async Task<List<long>> AddSplit(TEntity entity)
    {
        var insert = _db.Insertable(entity).SplitTable();
        // 插入并返回雪花 ID 并且自动赋值 ID　
        return await insert.ExecuteReturnSnowflakeIdListAsync();
    }
    
    public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(
        Expression<Func<T, T2, T3, object[]>> joinExpression,
        Expression<Func<T, T2, T3, TResult>> selectExpression,
        Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
    {
        if (whereLambda == null)
        {
            return await _db.Queryable(joinExpression).Select(selectExpression).ToListAsync();
        }

        return await _db.Queryable(joinExpression).Where(whereLambda).Select(selectExpression).ToListAsync();
    }
}