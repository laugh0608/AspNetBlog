using AspNetBlog.Common;
using AspNetBlog.Common.Attribute;
using AspNetBlog.IService;
using AspNetBlog.Model;
using AspNetBlog.Model.Vo;
using AspNetBlog.Repository.Base;
using AutoMapper;

namespace AspNetBlog.Service;

public class DepartmentServices : BaseServices<Department, UserVo>, IDepartmentServices
{
    private readonly IBaseRepository<Department> _dal;

    public DepartmentServices(IMapper mapper, IBaseRepository<Department> baseRepository) : base(mapper, baseRepository)
    {
        _dal = baseRepository;
    }

    /// <summary>
    /// 测试使用同事务
    /// </summary>
    /// <returns></returns>
    [UseTran(Propagation = Propagation.Required)]
    public async Task<bool> TestTranPropagation2()
    {
        TimeSpan timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var id = timeSpan.TotalSeconds.ObjToLong();
        var insertDepartment = await _dal.Add(new Department()
        {
            Id = id,
            Name = $"department name {id}",
            CodeRelationship = "",
            OrderSort = 0,
            Status = true,
            IsDeleted = false,
            Pid = 0
        });

        // 如果操作的 ContextID 一致，那么就可以视为一个工作单元
        await Console.Out.WriteLineAsync($"db context id : {base.Db.ContextID}");

        return true;
    }
}