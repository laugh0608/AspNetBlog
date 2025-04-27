using AspNetBlog.Common.HttpContextUser;
using AspNetBlog.IService;
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
    private readonly IUser _user;

    public TenantController(IUser user, IBaseServices<BusinessTable, BusinessTableVo> bizServices)
    {
        _user = user;
        _bizServices = bizServices;
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

}