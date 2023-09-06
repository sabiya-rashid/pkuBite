using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using pkuBite;
using Microsoft.OpenApi.Models;
using pkuBite.Data.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using AutoWrapper;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependencies();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:issuer"],
        ValidAudience = builder.Configuration["JWT:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
    };
});

builder.Services.AddMvc().AddFluentValidation(mvcconfiguration => mvcconfiguration.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "PKU Bite",
            Version = "v1",
            Description = "APIs for PKU-Bite",
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Enter the Bearer Authroization, `Bearer Generated-JWT-Token`",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new ()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }

                 }, new string[]{ }
            }
        });
    });


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();
app.UseApiResponseAndExceptionWrapper();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
