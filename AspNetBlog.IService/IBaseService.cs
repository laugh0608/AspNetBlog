using System.Linq.Expressions;
using SqlSugar;

namespace AspNetBlog.IService;

public interface IBaseServices<TEntity, TVo> where TEntity : class
{
    ISqlSugarClient Db { get; }
    
    Task<List<long>> AddSplit(TEntity entity);
    
    Task<long> Add(TEntity entity);
    
    // Task<List<TVo>> Query();
    Task<List<TVo>> Query(Expression<Func<TEntity, bool>>? whereExpression = null);
    
    // Task<List<TEntity>> QuerySplit(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);
    Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);
    
    // 测试缓存查询
    Task<List<TVo>> QueryWithCache(Expression<Func<TEntity, bool>>? whereExpression = null);
}