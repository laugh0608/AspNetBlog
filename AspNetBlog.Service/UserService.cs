using AspNetBlog.IService;
using AspNetBlog.Model;
using AspNetBlog.Repository;

namespace AspNetBlog.Service;

// 服务层
public class UserService : IUserService
{
    public async Task<List<UserVo>> Query()
    {
        var userRepo = new UserRepository();
        var users = await userRepo.Query();
        return users.Select(d => new UserVo() { UserName = d.Name }).ToList();
    }
}