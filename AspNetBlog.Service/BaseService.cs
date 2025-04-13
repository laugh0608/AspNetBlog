using AspNetBlog.IService;
using AspNetBlog.Repository;
using AspNetBlog.Repository.Base;

namespace AspNetBlog.Service;

public class BaseServices<TEntity, TVo> : IBaseServices<TEntity, TVo> where TEntity : class, new()
{
    public async Task<List<TEntity>> Query()
    {
        var baseRepo = new BaseRepository<TEntity>();
        return await baseRepo.Query();
    }
}