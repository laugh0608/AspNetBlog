namespace AspNetBlog.IService;

// 也是一个泛型
public interface IBaseService<TEntity, TVo> where TEntity : class
{
    Task<List<TEntity>> Query();
}