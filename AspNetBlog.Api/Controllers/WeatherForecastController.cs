using AspNetBlog.IService;
using AspNetBlog.Model;
using AspNetBlog.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetBlog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMapper _mapper;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //             TemperatureC = Random.Shared.Next(-20, 55),
    //             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //         })
    //         .ToArray();
    //     
    // }
    [HttpGet(Name = "GetWeatherForecast")]
    // 演示如何调用服务层
    // public async Task<List<UserVo>> Get()
    public async Task<object> Get()
    {
        // var userService = new UserService();
        // var userList = await userService.Query();
        // return userList;
        
        // 测试一下拿到 Role
        var roleService = new BaseServices<Role,RoleVo>(_mapper);
        var roleList = await roleService.Query();
        return roleList;
        // 更改为依赖注入
        // var roleVoList2 = await _roleService.Query();
        // return roleVoList2;
    }
}