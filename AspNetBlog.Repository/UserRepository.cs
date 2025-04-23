using Newtonsoft.Json;
using AspNetBlog.Model;

namespace AspNetBlog.Repository;

// 仓储层，只负责和数据库进行交互
public class UserRepository : IUserRepository
{
    public async Task<List<SysUserInfo>> Query()
    {
        // 手动定义一个 await Task
        await Task.CompletedTask;
        // 手动传入一个数据
        var data = "[{\"Id\": 18,\"Name\":\"luobo\"}]";
        // 并序列化
        return JsonConvert.DeserializeObject<List<SysUserInfo>>(data) ?? new List<SysUserInfo>();
    }
}