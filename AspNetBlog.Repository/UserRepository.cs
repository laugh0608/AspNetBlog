using Newtonsoft.Json;
using AspNetBlog.Model;

namespace AspNetBlog.Repository;

public class UserRepository : IUserRepository
{
    public async Task<List<User>> Query()
    {
        // 手动定义一个 await Task
        await Task.CompletedTask;
        // 手动传入一个数据
        var data = "[{\"Name\":\"luobo\"}]";
        // 并序列化
        return JsonConvert.DeserializeObject<List<User>>(data) ?? new List<User>();
    }
}