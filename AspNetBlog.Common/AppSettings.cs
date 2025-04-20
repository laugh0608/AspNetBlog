using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace AspNetBlog.Common;

// appsettings.json 操作类
public class AppSettings
{
     public static IConfiguration Configuration { get; set; }
     static string ContentPath { get; set; }

     public AppSettings(string contentPath)
     {
         string path = "appsettings.json";

         // 如果把配置文件是根据环境变量来分开了，可以这样写
         // Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

         Configuration = new ConfigurationBuilder()
             .SetBasePath(contentPath)
             .Add(new JsonConfigurationSource
             {
                 Path = path,
                 Optional = false,
                 ReloadOnChange = true
             }) // 这样的话，可以直接读目录里的 json 文件，而不是 bin 文件夹下的，所以不用修改复制属性
             .Build();
     }

     public AppSettings(IConfiguration configuration)
     {
         Configuration = configuration;
     }
     
     // 封装要操作的字符，sections 节点配置
     public static string App(params string[] sections)
     {
         try
         {
             if (sections.Any())
             {
                 return Configuration[string.Join(":", sections)];
             }
         }
         catch (Exception)
         {
         }

         return "";
     }
     
     // 递归获取配置信息数组
     public static List<T> App<T>(params string[] sections)
     {
         List<T> list = new List<T>();
         // 引用 Microsoft.Extensions.Configuration.Binder 包
         Configuration.Bind(string.Join(":", sections), list);
         return list;
     }
     
     // 根据路径  configuration["App:Name"];
     public static string GetValue(string sectionsPath)
     {
         try
         {
             return Configuration[sectionsPath];
         }
         catch (Exception)
         {
         }

         return "";
     }
}