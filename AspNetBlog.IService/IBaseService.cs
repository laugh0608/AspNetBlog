using SqlSugar;

namespace AspNetBlog.IService;

public interface IBaseServices<TEntity, TVo> where TEntity : class
{
    ISqlSugarClient Db { get; }
    
    Task<long> Add(TEntity entity);
    
    Task<List<TVo>> Query();
}