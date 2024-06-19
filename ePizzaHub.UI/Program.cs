using ePizzaHub.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using WebMarkupMin.AspNetCore8;

var builder = WebApplication.CreateBuilder(args);

//logging
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(builder.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();
// register dependencies
ConfigureDependencies.RegisterDependencies(builder.Services, builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.Cookie.Name = "ePizzaHub";
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/LogOut";
        option.AccessDeniedPath = "/Account/UnAuthorize";
        option.SlidingExpiration = true;
    });
//html compression setup
builder.Services.AddWebMarkupMin(options =>
{
    options.AllowMinificationInDevelopmentEnvironment = true;
    options.AllowCompressionInDevelopmentEnvironment = true;
    options.DisablePoweredByHttpHeaders = true;
}).AddHtmlMinification(options =>
{
    options.MinificationSettings.RemoveRedundantAttributes = true;
    options.MinificationSettings.MinifyInlineJsCode = true;
    options.MinificationSettings.MinifyInlineCssCode = true;
    options.MinificationSettings.MinifyEmbeddedJsonData = true;
    options.MinificationSettings.MinifyEmbeddedCssCode = true;
}).AddHttpCompression();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 7; //Secs*Mins*Hrs*Days
        ctx.Context.Response.Headers["cache-control"] =
            "public, max-age=" + durationInSeconds;
    }
});

app.UseRouting();
app.UseWebMarkupMin();//compressing html

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
