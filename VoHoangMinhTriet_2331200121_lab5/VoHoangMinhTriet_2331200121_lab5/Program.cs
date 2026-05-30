using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using VoHoangMinhTriet_2331200121_lab5.Controllers.Authorization.Handlers;
using VoHoangMinhTriet_2331200121_lab5.Controllers.Authorization.Requirements;
using VoHoangMinhTriet_2331200121_lab5.Models.Context;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddSingleton<IAuthorizationHandler, MinimumMembershipHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ActiveUserOnly", policy => policy.RequireClaim("isActive", "1"));
    options.AddPolicy("AdminOrLibrarian", policy => policy.RequireRole("Admin", "Librarian"));
    options.AddPolicy("MinimumMembership", policy => policy.Requirements.Add(new MinimumMembershipRequirement(30)));
    options.AddPolicy("ManageActiveCategories", policy => policy.RequireRole("Admin").RequireClaim("CanManageCategories", "True"));
    options.AddPolicy("VerifiedEmailOnly", policy => policy.RequireClaim("EmailVerified", "True"));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // 1. Define the "Bearer" security scheme
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LibraryManagementContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();
app.MapGet("/", () => "library management API");
app.Run();
