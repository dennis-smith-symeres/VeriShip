using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RefitClient;
using Serilog;
using VeriShip.Application;
using VeriShip.Application.Common;
using VeriShip.Infrastructure;
using VeriShip.WebApp;
using VeriShip.WebApp.Components;
using VeriShip.WebApp.Services;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) 
  
    .CreateLogger();
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration);
  
});
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration);

builder.AddkeyVault();


builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();


builder.Services.AddAuthorization();
builder.Services.AddTelerikBlazor();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddScoped<INotifications, Notifications>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDataProtection()
    .SetApplicationName(AppSettings.ApplicationName);

builder.Services.AddSingleton<IUsersStateContainer, UsersStateContainer>();
builder.Services.AddScoped<CircuitHandler, TrackingCircuitHandler>();

// builder.Services.Configure<RefitOptions>(builder.Configuration.GetSection("Signals"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForErrors: true);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();