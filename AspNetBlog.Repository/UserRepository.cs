using Newtonsoft.Json;
using AspNetBlog.Model;
using AspNetBlog.Repository.Base;
using AspNetBlog.Repository.UnitOfWork;
using SqlSugar;

namespace AspNetBlog.Repository;

// 仓储层，只负责和数据库进行交互
// public class UserRepository : IUserRepository
// {
//     public async Task<List<SysUserInfo>> Query()
//     {
//         // 手动定义一个 await Task
//         await Task.CompletedTask;
//         // 手动传入一个数据
//         var data = "[{\"Id\": 18,\"Name\":\"luobo\"}]";
//         // 并序列化
//         return JsonConvert.DeserializeObject<List<SysUserInfo>>(data) ?? new List<SysUserInfo>();
//     }
// }

public class UserRepository : BaseRepository<SysUserInfo>, IUserRepository
{
    public UserRepository(IUnitOfWorkManage unitOfWorkManage) : base(unitOfWorkManage)
    {
    }

    public async Task<List<SysUserInfo>> Query()
    {
        await Task.CompletedTask;
        var data = "[{\"Id\": 18,\"Name\":\"laozhang\"}]";
        return JsonConvert.DeserializeObject<List<SysUserInfo>>(data) ?? new List<SysUserInfo>();
    }

    public async Task<List<RoleModulePermission>> RoleModuleMaps()
    {
        return await QueryMuch<RoleModulePermission, Modules, Role, RoleModulePermission>(
            (rmp, m, r) => new object[] {
                JoinType.Left, rmp.ModuleId == m.Id,
                JoinType.Left,  rmp.RoleId == r.Id
            },

            (rmp, m, r) => new RoleModulePermission()
            {
                Role = r,
                Module = m,
                IsDeleted = rmp.IsDeleted
            },

            (rmp, m, r) => rmp.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false
        );
    }

}