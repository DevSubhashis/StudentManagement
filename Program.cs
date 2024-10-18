using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManagement.Data;
using StudentManagement.Models;
using StudentManagement.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add DbContext for SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StudentManagementConnection")));

// Add repositories to the DI container
builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
// builder.Services.AddScoped<IRepository<Teacher>, TeacherRepository>();
// builder.Services.AddScoped<IRepository<Classroom>, ClassroomRepository>();
// builder.Services.AddScoped<IRepository<Profile>, ProfileRepository>();
// builder.Services.AddScoped<IRepository<Subject>, SubjectRepository>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI support (for testing APIs)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
