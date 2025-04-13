namespace AspNetBlog.Repository.Base;

// 仓储基类接口
// 添加一个 TEntity 泛型，添加到仓储接口里
public interface IBaseRepository<TEntity> where TEntity : class
{
    // 然后给 TEntity 返回一个 List
    Task<List<TEntity>> Query();
}