namespace AspNetBlog.IService;

public interface IBaseServices<TEntity, TVo> where TEntity : class
{
    Task<long> Add(TEntity entity);
    Task<List<TVo>> Query();
}