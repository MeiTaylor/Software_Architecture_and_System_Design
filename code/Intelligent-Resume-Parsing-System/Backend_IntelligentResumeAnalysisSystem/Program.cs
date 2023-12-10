using Microsoft.EntityFrameworkCore;
using Backend_IntelligentResumeAnalysisSystem.Data; // 替换为你的 MyDbContext 命名空间
using Backend_IntelligentResumeAnalysisSystem.Data.Repository;
using Backend_IntelligentResumeAnalysisSystem.Services;
using Backend_IntelligentResumeAnalysisSystem.Models;

var builder = WebApplication.CreateBuilder(args);


// 设置全局的 PYTHONPATH 环境变量


string pythonPath = @"C:\Users\86178\.conda\envs\resume\python.exe";
Environment.SetEnvironmentVariable("PYTHONPATH", pythonPath);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 30))));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<IJobPositionRepository, JobPositionRepository>();
builder.Services.AddScoped<IJobPositionService, JobPositionService>();
//builder.Services.AddScoped<CompanyService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(cfg =>
{
    cfg.AllowAnyOrigin(); //对应跨域请求的地址
    cfg.AllowAnyMethod(); //对应请求方法的Method
    cfg.AllowAnyHeader(); //对应请求方法的Headers
                          //cfg.AllowCredentials(); //对应请求的withCredentials 值
});

app.UseAuthorization(); 

app.MapControllers();

app.Run();
