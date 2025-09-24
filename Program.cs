using Microsoft.EntityFrameworkCore;
using WebsiteQLNhaTro.Models;
using WebsiteQLNhaTro.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add default authorization policy requiring authentication for all endpoints
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddControllersWithViews();

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer not configured"),
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience not configured"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not configured")))
        };
    });

// Add services to DI container
builder.Services.AddScoped<WebsiteQLNhaTro.Services.JwtService>();
builder.Services.AddScoped<WebsiteQLNhaTro.Services.UserService>();
builder.Services.AddScoped<WebsiteQLNhaTro.Services.ApartmentService>();
builder.Services.AddScoped<WebsiteQLNhaTro.Services.ApartmentRoomService>();
builder.Services.AddScoped<WebsiteQLNhaTro.Services.RoomFeeCollectionService>();
builder.Services.AddScoped<WebsiteQLNhaTro.Services.ActionLogService>();
builder.Services.AddScoped<WebsiteQLNhaTro.Services.EmailService>();
builder.Services.AddScoped<WebsiteQLNhaTro.Services.StatisticsService>();
builder.Services.AddHostedService<WebsiteQLNhaTro.Services.UnpaidNotificationBackgroundService>();

// Configure and register SMTP settings
var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Register AppDbContext with MySQL
builder.Services.AddDbContext<WebsiteQLNhaTro.Entities.AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0))
    )
);


var app = builder.Build();

// Global exception handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync($"{{\"message\": \"{ex.Message}\"}}");
    }
});
// Configure the HTTP request pipeline.
// app.UseHttpRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Disabled to run only HTTP
app.UseRouting();

// Thêm middleware authentication và authorization
app.UseAuthentication();
app.UseAuthorization();

// Static files không yêu cầu xác thực
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

