namespace AspNetBlog.IService;

// 也是一个泛型
public interface IBaseServices<TEntity, TVo> where TEntity : class
{
    Task<List<TVo>> Query();
}