using DataAccess.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApp.Controllers;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITokenService, TokenService>();

using IHost host = Host.CreateDefaultBuilder(args).Build();

// Ask the service provider for the configuration abstraction.
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();


// Get values from the config given their key and their target type.
string BaseUrl = config.GetSection("BaseUrl").Value;
 static string GetBaseUrl(string baseUrl)
{
    return baseUrl;
}
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(double.Parse(config["SessionTimeOutInterval"]));

    options.Cookie.IsEssential = true;
});

// Add services to the container.
// Multi Languages
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews().AddViewLocalization
    (LanguageViewLocationExpanderFormat.SubFolder)
    .AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options => {
    var supportedCultures = new[] {  "en-US", "ar" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});
//
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
    };
});

builder.Services
             .AddControllersWithViews()
             .AddJsonOptions(options =>
                 options.JsonSerializerOptions.PropertyNamingPolicy = null);


builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Multi Languages
var supportedCultures = new[] {"en-US", "ar" };

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.Use(async (context, next) =>
{
    var token = context.Session.GetString("Token");
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }
    await next();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignUpView}/{id?}");

app.Run();
