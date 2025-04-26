using SqlSugar;

namespace AspNetBlog.IService;

public interface IBaseServices<TEntity, TVo> where TEntity : class
{
    ISqlSugarClient Db { get; }
    
    Task<List<long>> AddSplit(TEntity entity);
    
    Task<long> Add(TEntity entity);
    
    Task<List<TVo>> Query();
    
    Task<List<TEntity>> QuerySplit(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);
}