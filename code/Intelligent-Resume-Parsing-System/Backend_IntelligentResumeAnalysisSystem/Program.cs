using Microsoft.EntityFrameworkCore;
using Backend_IntelligentResumeAnalysisSystem.Data; // �滻Ϊ��� MyDbContext �����ռ�
using Backend_IntelligentResumeAnalysisSystem.Data.Repository;
using Backend_IntelligentResumeAnalysisSystem.Services;
using Backend_IntelligentResumeAnalysisSystem.Models;

var builder = WebApplication.CreateBuilder(args);


// ����ȫ�ֵ� PYTHONPATH ��������


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
    cfg.AllowAnyOrigin(); //��Ӧ��������ĵ�ַ
    cfg.AllowAnyMethod(); //��Ӧ���󷽷���Method
    cfg.AllowAnyHeader(); //��Ӧ���󷽷���Headers
                          //cfg.AllowCredentials(); //��Ӧ�����withCredentials ֵ
});

app.UseAuthorization(); 

app.MapControllers();

app.Run();
