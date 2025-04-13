using AspNetBlog.IService;
using AspNetBlog.Repository;

namespace AspNetBlog.Service;

public class BaseService<TEntity, TVo> : IBaseService<TEntity, TVo> where TEntity : class, new()
{
    public async Task<List<TEntity>> Query()
    {
        var baseRepo = new BaseRepository<TEntity>();
        return await baseRepo.Query();
    }
}