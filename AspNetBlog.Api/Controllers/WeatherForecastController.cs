using AspNetBlog.Common;
using AspNetBlog.Common.Caches;
using AspNetBlog.Common.Core;
using AspNetBlog.Common.Option;
using AspNetBlog.IService;
using AspNetBlog.Model;
// using AspNetBlog.Service;
// using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
    private readonly IBaseServices<Role, RoleVo> _roleServices;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IOptions<RedisOptions> _redisOption;

    private readonly ICaching _caching;
    // private readonly IMapper _mapper;
    
    // 属性注册
    public IBaseServices<Role,RoleVo> RoleServiceObj { get; set; }

    // 相当于告诉依赖注入容器，我需要什么参数，你给我传输过来，然后在下面直接使用就可以
    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IBaseServices<Role,RoleVo> roleServices,
        IServiceScopeFactory scopeFactory,
        IOptions<RedisOptions> redisOption,
        ICaching caching) // IMapper mapper
    {
        _logger = logger;
        _roleServices = roleServices;
        _scopeFactory = scopeFactory;
        _redisOption = redisOption;
        _caching = caching;
        // _mapper = mapper;
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
        // var roleService = new BaseServices<Role,RoleVo>(_mapper);
        // var roleList = await roleService.Query();
        // 更改为依赖注入
        // var roleList = await _roleServices.Query();
        
        // 更改为 Autofac，测试一下多次调用实体的 hash 值是否发生变化
        // var roleList = await _roleServices.Query();
        // Console.WriteLine(_roleServices.GetHashCode());
        // var roleList2 = await _roleServices.Query();
        // Console.WriteLine(_roleServices.GetHashCode()); // Hash 值并不会发生变化
        
        // Hash 值会发生变化的方式：
        // using var scope = _scopeFactory.CreateScope();
        // var dataStatisticService = scope.ServiceProvider.GetRequiredService<IBaseServices<Role, RoleVo>>();
        // var roleList = await dataStatisticService.Query();
        // var dataStatisticService2 = scope.ServiceProvider.GetRequiredService<IBaseServices<Role, RoleVo>>();
        // var roleList2 = await dataStatisticService.Query();
        
        // // 属性注册的方式：
        // var roleList = await RoleServiceObj.Query();
        // // appsettings 配置获取
        // var redisEnable = AppSettings.App(new []{"Redis", "Enable"});
        // // 类似的还有第二种获取的方式：
        // var redisConnectionString = AppSettings.GetValue("Redis:ConnectionString");
        // Console.WriteLine($"Enable: {redisEnable} , RedisConnectionString: {redisConnectionString}");
        // // 另一种 option 方法获取配置：
        // var redisOption = _redisOption.Value;
        // Console.WriteLine(JsonConvert.SerializeObject(redisOption));
        
        // 测试非依赖注入方式获取 App
        // var roleServiceObjNew = App.GetService<IBaseServices<Role, RoleVo>>(false);
        // var roleList = await roleServiceObjNew.Query();
        // var redisOptions = App.GetOptions<RedisOptions>();
        // Console.WriteLine(redisOptions);
        
        // // 测试缓存，先获取当前所有的缓存 key
        // var cacheKey = "cache-key";
        // List<string> cacheKeys = await _caching.GetAllCacheKeysAsync();
        // await Console.Out.WriteLineAsync("全部keys-->" + JsonConvert.SerializeObject(cacheKeys));
        // // 测试添加一个缓存
        // await Console.Out.WriteLineAsync("添加了一个缓存");
        // await _caching.SetStringAsync(cacheKey, "hello world");
        // await Console.Out.WriteLineAsync("全部keys-->" + JsonConvert.SerializeObject(await _caching.GetAllCacheKeysAsync()));
        // await Console.Out.WriteLineAsync("当前key内容-->" + JsonConvert.SerializeObject(await _caching.GetStringAsync(cacheKey)));
        // // 测试删除这个缓存
        // await Console.Out.WriteLineAsync("删除key");
        // await _caching.RemoveAsync(cacheKey);
        // await Console.Out.WriteLineAsync("全部keys-->" + JsonConvert.SerializeObject(await _caching.GetAllCacheKeysAsync()));
        
        // 测试数据库链接
        var roleList = await RoleServiceObj.Query();
        Console.WriteLine("Api Request end...");
        return roleList;
    }
}