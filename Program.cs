 global using Microsoft.EntityFrameworkCore;
global using API.Interfaces;
global using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using API.Extensions;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices( builder.Configuration );
builder.Services.AddIdentityServices( builder.Configuration );

var app = builder.Build();

app.UseMiddleware<ExceptionMiddlewares>();

app.UseCors(
    cors => cors.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins( "http://localhost:4200" )
);

app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
