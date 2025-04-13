using AspNetBlog.Model;

namespace AspNetBlog.IService;

// 服务接口层
public interface IUserService
{
    Task<List<UserVo>> Query();
}