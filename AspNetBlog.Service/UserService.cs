﻿using AspNetBlog.Common;
using AspNetBlog.Common.Attribute;
using AspNetBlog.IService;
using AspNetBlog.Model;
using AspNetBlog.Repository.Base;
using AutoMapper;

namespace AspNetBlog.Service;

// 服务层
// public class UserService : IUserService
// {
//     public async Task<List<UserVo>> Query()
//     {
//         var userRepo = new UserRepository();
//         var users = await userRepo.Query();
//         return users.Select(d => new UserVo() { UserName = d.Name }).ToList();
//     }
// }

public class UserService : BaseServices<SysUserInfo, UserVo>, IUserService
{
    private readonly IDepartmentServices _departmentServices;

    public UserService(IDepartmentServices departmentServices, IMapper mapper, IBaseRepository<SysUserInfo> baseRepository) : base(mapper, baseRepository)
    {
        _departmentServices = departmentServices;
    }


    /// <summary>
    /// 测试使用同事务
    /// </summary>
    /// <returns></returns>
    [UseTran(Propagation = Propagation.Required)]
    public async Task<bool> TestTranPropagation()
    {
        await Console.Out.WriteLineAsync($"db context id : {base.Db.ContextID}");
        var sysUserInfos = await base.Query();

        TimeSpan timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var id = timeSpan.TotalSeconds.ObjToLong();
        var insertSysUserInfo = await base.Add(new SysUserInfo()
        {
            Id = id,
            Name = $"user name {id}",
            Status = 0,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            CriticalModifyTime = DateTime.Now,
            LastErrorTime = DateTime.Now,
            ErrorCount = 0,
            Enable = true,
            TenantId = 0,
        });

        await _departmentServices.TestTranPropagation2();

        return true;
    }
}