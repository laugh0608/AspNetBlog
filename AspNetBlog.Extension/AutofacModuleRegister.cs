using System.Reflection;
using AspNetBlog.IService;
using AspNetBlog.Repository.Base;
using AspNetBlog.Service;
using Autofac;

namespace AspNetBlog.Extension;

public class AutofacModuleRegister: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var basePath = AppContext.BaseDirectory;
        var serviceDllFile = Path.Combine(basePath, "AspNetBlog.Service.dll");
        var repositoryDllFile = Path.Combine(basePath, "AspNetBlog.Repository.dll");
        
        // 注册仓储
        builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();
        // 注册服务
        builder.RegisterGeneric(typeof(BaseServices<,>)).As(typeof(IBaseServices<,>)).InstancePerDependency();

        // 获取 Service.dll 程序集服务，并注册
        var assemblesServices = Assembly.LoadFrom(serviceDllFile);
        builder.RegisterAssemblyTypes(assemblesServices)
            .AsImplementedInterfaces()  // 接口
            .InstancePerDependency()    // 顺带模式
            .PropertiesAutowired();     // 属性

        // 获取 Repository.dll 程序集服务，并注册
        var assemblesRepository = Assembly.LoadFrom(repositoryDllFile);
        builder.RegisterAssemblyTypes(assemblesRepository)
            .AsImplementedInterfaces()
            .PropertiesAutowired()
            .InstancePerDependency();
    }
}