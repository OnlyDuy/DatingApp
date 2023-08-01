using System.Text;
using API.Data;
using API.Extensions;
using API.interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddIdentityService(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// khi cố gắng truy cập vào địa chỉ của http thì sẽ luôn trở thành https
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

// Xác thực
app.UseAuthentication();

// thiết lập ủy quyền cho ứng dụng
app.UseAuthorization();

app.MapControllers();

app.Run();
