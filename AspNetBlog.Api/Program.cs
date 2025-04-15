using AspNetBlog.Api.Extensions;
using AspNetBlog.IService;
using AspNetBlog.Repository.Base;
using AspNetBlog.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 泛型对象关系映射引入 Mapper 依赖注入
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
AutoMapperConfig.RegisterMappings();

// 实现原生依赖注入
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseServices<,>), typeof(BaseServices<,>));

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