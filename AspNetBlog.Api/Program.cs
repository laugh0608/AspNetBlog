using System.Text;
using AspNetBlog.Api.Extensions;
using AspNetBlog.Common;
using AspNetBlog.Common.Core;
using AspNetBlog.Extension.ServiceExtensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

// 原生的依赖注入方法和容器
var builder = WebApplication.CreateBuilder(args);
// 更改为 Autofac 注入
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule<AutofacModuleRegister>();    // 模型注册
        builder.RegisterModule<AutofacPropertityModuleReg>();   // 属性注册
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        // 拿到 app 配置项
        hostingContext.Configuration.ConfigureApplication();
    });

// 拿到 web 环境变量
builder.ConfigureApplication();
// Add services to the container.
// 添加激活控制器，如果控制器不走容器的话，是没办法将容器内的属性通过加载的方式进行完成
builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 泛型对象关系映射引入 Mapper 依赖注入
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
AutoMapperConfig.RegisterMappings();
// 实现原生依赖注入
// 更改为上面的 Autofac 注入
// builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
// builder.Services.AddScoped(typeof(IBaseServices<,>), typeof(BaseServices<,>));

// appsettings 配置
builder.Services.AddSingleton(new AppSettings(builder.Configuration));
builder.Services.AddAllOptionRegister();
// 配置缓存
builder.Services.AddCacheSetup();
// 配置数据库 ORM
builder.Services.AddSqlsugarSetup();
// 配置 JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Blog.Core",
            ValidAudience = "wr",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs"))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Client", policy => policy.RequireClaim("iss", "Blog.Core").Build());
    options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin").Build());
    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("SuperAdmin", "System"));
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();
// 从获取的配置项配置 app 实例，拿到 service
app.ConfigureApplication();
// 配置 app 启动
app.UseApplicationSetup();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();