using AspNetBlog.Common.HttpContextUser;
using AspNetBlog.IService;
using AspNetBlog.Model;
using AspNetBlog.Model.Tenants;
using AspNetBlog.Model.Vo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace AspNetBlog.Api.Controllers;

/// <summary>
/// 多租户-Id 方案 测试
/// </summary>
[Produces("application/json")]
[Route("api/[controller]/[action]")]
[Authorize]
public class TenantController : ControllerBase
{
    private readonly IBaseServices<BusinessTable, BusinessTableVo> _bizServices;
    private readonly IBaseServices<MultiBusinessTable, MultiBusinessTableVo> _multiBusinessService;
    private readonly IBaseServices<SubLibraryBusinessTable, SubLibraryBusinessTableVo> _subLibBusinessService;
    private readonly IBaseServices<SysTenant, SysTenantVo> _sysTenantService;
    private readonly IUser _user;

    public TenantController(IUser user, IBaseServices<BusinessTable, BusinessTableVo> bizServices,
        IBaseServices<MultiBusinessTable, MultiBusinessTableVo> multiBusinessService,
        IBaseServices<SubLibraryBusinessTable, SubLibraryBusinessTableVo> subLibBusinessService,
        IBaseServices<SysTenant, SysTenantVo> sysTenantService)
    {
        _user = user;
        _bizServices = bizServices;
        _multiBusinessService = multiBusinessService;
        _subLibBusinessService = subLibBusinessService;
        _sysTenantService = sysTenantService;
    }

    /// <summary>
    /// 获取租户下全部业务数据 <br/>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<object> GetAll()
    {
        return await _bizServices.Query();
    }
    
    /// <summary>
    /// 获取租户下全部业务数据（分表）
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<object> MultiBusinessByTable()
    {
        return await _multiBusinessService.Query();
    }
    
    /// <summary>
    /// 获取租户下全部业务数据（分库）
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<object> SubLibraryBusinessTable()
    {
        return await _subLibBusinessService.Query();
    }
    
    // 测试缓存查询，查询方法
    [HttpGet]
    public async Task<object> GetTenantCache()
    {
        return await _sysTenantService.QueryWithCache();
    }
    // 测试缓存查询，添加方法
    [HttpGet]
    public async Task<object> AddTenant()
    {
        return await _sysTenantService.Add(new SysTenant()
        {
            Id = SnowFlakeSingle.instance.getID(),
            Name = "test name",
            TenantType = TenantTypeEnum.Db,
            ConfigId = "test config",
            Status = false
        });
    }

}