using AspNetBlog.Common.HttpContextUser;
using AspNetBlog.IService;
using AspNetBlog.Model;
using AspNetBlog.Model.Tenants;
using AspNetBlog.Model.Vo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    private readonly IUser _user;

    public TenantController(IUser user, IBaseServices<BusinessTable, BusinessTableVo> bizServices,
        IBaseServices<MultiBusinessTable, MultiBusinessTableVo> multiBusinessService,
        IBaseServices<SubLibraryBusinessTable, SubLibraryBusinessTableVo> subLibBusinessService)
    {
        _user = user;
        _bizServices = bizServices;
        _multiBusinessService = multiBusinessService;
        _subLibBusinessService = subLibBusinessService;
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

}