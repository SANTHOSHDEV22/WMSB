using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WMSB.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("workerApp",
        builder => builder.WithOrigins("https://localhost:44372")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WorkerDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddSwaggerGen((options) =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "Version 1",
        Title = "Test API V1",
        Description = "WMS"
    });

});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("workerApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
