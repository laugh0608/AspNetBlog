using AspNetBlog.Model;

namespace AspNetBlog.Repository;

public interface IUserRepository
{
    Task<List<User>> Query();
}