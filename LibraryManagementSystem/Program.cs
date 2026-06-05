using LibraryManagementSystem.API.Middlewares;
using LibraryManagementSystem.Common.Helpers;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Service.Implementations;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using LibraryManagementSystem.Store.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);


#region Database

builder.Services.AddDbContext<LibraryDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region Dependency Injection

builder.Services.AddScoped<IAuthStore, AuthStore>();
builder.Services.AddScoped<IUserStore, UserStore>();
builder.Services.AddScoped<IAuthorStore, AuthorStore>();
builder.Services.AddScoped<ICategoryStore, CategoryStore>();
builder.Services.AddScoped<IMemberStore, MemberStore>();
builder.Services.AddScoped<IBookStore, BookStore>();
builder.Services.AddScoped<IBookIssueStore, BookIssueStore>();
builder.Services.AddScoped<IFineStore, FineStore>();
builder.Services.AddScoped<IErrorLogStore, ErrorLogStore>();



builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookIssueService, BookIssueService>();
builder.Services.AddScoped<IFineService, FineService>();

builder.Services.AddScoped<JwtHelper>();

#endregion

#region Controllers

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

#endregion

#region JWT Authentication

builder.Services.AddAuthentication(
JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
    new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,


            ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

        ValidAudience =
                    builder.Configuration["Jwt:Audience"],

        IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
    };
});


builder.Services.AddAuthorization();

#endregion

#region Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Library Management API",
            Version = "v1"
        });

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT Token"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });

    var xmlFile =
            $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

builder.Host.UseSerilog();
#endregion

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();