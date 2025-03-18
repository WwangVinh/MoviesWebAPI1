using Microsoft.EntityFrameworkCore;
using MoviesDB.Interfaces;
using MoviesDB.Models;
using MoviesDB.Repositories;
using MoviesWebAPI.Services;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Thêm DbContext
builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDBConnection")));

// Đăng ký User Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Cấu hình Swagger
builder.Services.AddSwaggerGen();

// Cấu hình JSON Serializer để tránh vòng lặp tham chiếu
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false; // Giữ cấu hình đúng
}).AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();

// Thêm các dịch vụ khác
builder.Services.AddControllers();
builder.Services.AddSingleton<JwtService>();

// Cấu hình CORS (nếu cần)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Cấu hình pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins"); // Cho phép CORS (nếu cần)
app.UseAuthorization();
app.MapControllers();

app.Run();
