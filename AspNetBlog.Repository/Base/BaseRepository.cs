using Newtonsoft.Json;

namespace AspNetBlog.Repository.Base;

// 继承 IBaseRepository 基类
// 目的是通过传递不同的实体对象，将数据库查询到的内容进行不同的转化和推送
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
{
    public async Task<List<TEntity>> Query()
    {
        // 手动定义一个 await Task
        await Task.CompletedTask;
        // 手动传入一个数据
        var data = "[{\"Id\": 18,\"Name\":\"nameName\"}]";
        // 并序列化
        return JsonConvert.DeserializeObject<List<TEntity>>(data) ?? new List<TEntity>();
    }
}