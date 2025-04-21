using AspNetBlog.Common.Caches;
using AspNetBlog.Common.Core;
using AspNetBlog.Common.Option;
using AspNetBlog.Extension.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AspNetBlog.Extension.ServiceExtensions;

public static class CacheSetup
{
    /// <summary>
    /// 统一注册缓存
    /// </summary>
    /// <param name="services"></param>
    public static void AddCacheSetup(this IServiceCollection services)
    {
        // 获取 App 的配置项，使用非依赖注入方式
        var cacheOptions = App.GetOptions<RedisOptions>();
        // 配置中 redis 状态为 enable
        if (cacheOptions.Enable)
        {
            // 配置启动 Redis 服务，虽然可能影响项目启动速度，但是不能在运行的时候报错，所以是合理的
            // 这两行等价于下面使用内存缓存的 services.AddMemoryCache(); 语句
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                // 获取连接字符串
                var configuration = ConfigurationOptions.Parse(cacheOptions.ConnectionString, true);
                configuration.ResolveDns = true;
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddSingleton<ConnectionMultiplexer>(p => p.GetService<IConnectionMultiplexer>() as ConnectionMultiplexer);
            
            // 使用 Redis
            services.AddStackExchangeRedisCache(options =>
            {
                options.ConnectionMultiplexerFactory = () => Task.FromResult(App.GetService<IConnectionMultiplexer>(false));
                if (!string.IsNullOrEmpty(cacheOptions.InstanceName)) options.InstanceName = cacheOptions.InstanceName;
            });

            services.AddTransient<IRedisBasketRepository, RedisBasketRepository>();
        }
        else // 配置中 redis 状态为 false，使用内存
        {
            // 添加一个内存缓存
            services.AddMemoryCache();
            // 来自接口 IDistributedCache
            services.AddDistributedMemoryCache();
        }

        services.AddSingleton<ICaching, Caching>();
    }
}