
using System.Reflection;
using System.Text;
using AutoMapper;
using JwtAppBack.Core.Application.Interfaces;
using JwtAppBack.Core.Application.Mappings;
using JwtAppBack.Persistance.Context;
using JwtAppBack.Persistance.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
//! JWT
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters(){
        ValidAudience = "http://localhost",
        ValidIssuer = "http://localhost",
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ferhatferhatferhatferhatferhat.1")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };
});
//!
//?
//! Repositories
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//!
//?
//! MEDİATR
services.AddMediatR(Assembly.GetExecutingAssembly());
//!
//?
//! AutoMapper
services.AddAutoMapper(opt =>
{
    opt.AddProfiles(new List<Profile> {
        new ProductMapper(),
        new CategoryMapper()
    });


});

//!
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddDbContext<JwtAppContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("local"));
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
