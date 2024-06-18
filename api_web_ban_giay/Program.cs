using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using api_web_ban_giay.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<api_web_ban_giay_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("api_web_ban_giayContext") ?? throw new InvalidOperationException("Connection string 'api_web_ban_giayContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
    builder.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
