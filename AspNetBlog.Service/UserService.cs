using AspNetBlog.IService;
using AspNetBlog.Model;
using AspNetBlog.Repository;

namespace AspNetBlog.Service;

// 服务层
public class UserService : IUserService
{
    public async Task<List<UserViewObject>> Query()
    {
        var userRepo = new UserRepository();
        var users = await userRepo.Query();
        return users.Select(d => new UserViewObject() { UserName = d.Name }).ToList();
    }
}