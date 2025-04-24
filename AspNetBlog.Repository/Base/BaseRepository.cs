using System.Reflection;
using AspNetBlog.Repository.UnitOfWork;
using Newtonsoft.Json;
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

            return db;
        }
    }
    public BaseRepository(IUnitOfWorkManage unitOfWorkManage)
    {
        _unitOfWorkManage = unitOfWorkManage;
        _dbBase = unitOfWorkManage.GetDbClient();
    }

    public async Task<List<TEntity>> Query()
    {
        await Console.Out.WriteLineAsync(Db.GetHashCode().ToString());
        return await _db.Queryable<TEntity>().ToListAsync();
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
}