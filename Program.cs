using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));


builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();
//builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
//{
//    options.Events.OnAuthenticationFailed = context =>
//    {
//        context.Response.Redirect("/MicrosoftIdentity/Account/Error?message=" + Uri.EscapeDataString(context.Exception.Message));
//        context.HandleResponse(); // Prevent default error handling
//        return Task.CompletedTask;
//    };
//});


builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Events.OnAuthenticationFailed = context =>
    {
        context.Response.Redirect("/MicrosoftIdentity/Account/Error?message=" + Uri.EscapeDataString(context.Exception.Message));
        context.HandleResponse();
        return Task.CompletedTask;
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Accept issuers that contain 'tfp'
        IssuerValidator = (issuer, token, parameters) =>
        {
            if (issuer.Contains("tfp"))
                return issuer;
            throw new SecurityTokenInvalidIssuerException("Invalid issuer");
        }
    };
});



var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
