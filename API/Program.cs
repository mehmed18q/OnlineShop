using API;
using Application;
using Application.AuthenticateCommandQuery.Command;
using Application.ProductCommandQuery.Command;
using AutoMapper;
using Core;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    Args = args,
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

#region Configuration
builder.Services.AddOptions();
builder.Services.Configure<Configs>(builder.Configuration.GetSection(nameof(Configs)));
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

#region Mediator
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaveProductCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));
#endregion

#region Register DbContext
string connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    _ = options.UseSqlServer(connectionString);
});
#endregion

#region Registerations
builder.Services.AddJWT();
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();
builder.Services.AddInfraUtility();
builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();
#endregion

#region Auto Mapper
MapperConfiguration config = new(cfg =>
{
    cfg.AddProfile(new AutoMapperConfig());
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

builder.Services.AddMemoryCache();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

#region Static File
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, builder.Configuration.GetValue<string>("MediaPath")!)),
    RequestPath = builder.Configuration.GetValue<string>("RequestMediaPath")!
});
#endregion

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
