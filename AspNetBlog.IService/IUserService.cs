using AspNetBlog.Model;
using AspNetBlog.Model.Vo;

namespace AspNetBlog.IService;

// 服务接口层
// public interface IUserService : IBaseServices<SysUserInfo, UserVo>
// {
//     Task<List<UserVo>> Query();
//     Task<bool> TestTranPropagation();
// }

public interface IUserService : IBaseServices<SysUserInfo, UserVo>
{
    Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
    Task<List<RoleModulePermission>> RoleModuleMaps();
    Task<bool> TestTranPropagation();
}