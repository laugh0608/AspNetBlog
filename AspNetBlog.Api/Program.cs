using AspNetBlog.Extension;
using Autofac;
using Autofac.Extensions.DependencyInjection;

// 原生的依赖注入方法和容器
var builder = WebApplication.CreateBuilder(args);
// 更改为 Autofac 注入
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule<AutofacModuleRegister>();
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 泛型对象关系映射引入 Mapper 依赖注入
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
AutoMapperConfig.RegisterMappings();

// 实现原生依赖注入
// builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
// builder.Services.AddScoped(typeof(IBaseServices<,>), typeof(BaseServices<,>));
// 更改为 Autofac 注入

var app = builder.Build();

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