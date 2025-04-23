using AspNetBlog.IService;
using AspNetBlog.Repository.Base;
using AutoMapper;
using SqlSugar;

namespace AspNetBlog.Service;

// TEntity 实体泛型，TVo 视图泛型
public class BaseServices<TEntity, TVo> : IBaseServices<TEntity, TVo> where TEntity : class, new()
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<TEntity> _baseRepository;
    public ISqlSugarClient Db => _baseRepository.Db;

    // 依赖注入，类似于一种注册表的形式
    public BaseServices(IMapper mapper, IBaseRepository<TEntity> baseRepository)
    {
        _mapper = mapper;
        _baseRepository = baseRepository;
    }
    
    // public async Task<List<TEntity>> Query()
    // {
    //     var baseRepo = new BaseRepository<TEntity>();
    //     return await baseRepo.Query();
    // }
    // 更改为泛型对象关系映射，不返回实体模型，返回映射后的视图模型
    public async Task<List<TVo>> Query()
    {
        // 从仓储层拿到数据对象，实体模型（通过链接数据库实例，拿到相对应的数据库中的数据）
        // 更改为依赖注入，不需要 new 了
        // var baseRepo = new BaseRepository<TEntity>();
        // 实体模型要暴露到外部就要转换为视图模型
        var entities = await _baseRepository.Query();
        // 检查一下多次调用时数据体是否会发生变化
        Console.WriteLine($"_baseRepository 实例 HashCode: {_baseRepository.GetHashCode()}"); // 结果值是一样的
        // 对象关系映射
        var llout = _mapper.Map<List<TVo>>(entities);
        return llout;
    }

    /// <summary>
    /// 向数据库中写入实体数据
    /// </summary>
    /// <param name="entity">博文实体类</param>
    /// <returns></returns>
    public async Task<long> Add(TEntity entity)
    {
        return await _baseRepository.Add(entity);
    }
}