using BlazorApp1.Components;
using BlazorApp1.Components.Account;
using BlazorApp1.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Models;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//todo list connection string
var TodoConnectionString = builder.Configuration.GetConnectionString("TodoConnection") ?? throw new InvalidOperationException("Connection string 'TodoConnectionString' not found.");
builder.Services.AddDbContext<ToDoDataContext>(options =>
    options.UseSqlite(TodoConnectionString));



builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthenticatedUser", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
    options.AddPolicy("RequireAdministratorRole", policy =>
    {
        policy.RequireRole("Admin");
    });
    options.AddPolicy("RequireSuperuserRole", policy =>
    {
        policy.RequireRole("Super");
    });
});
builder.WebHost.UseKestrel((context, serverOptions) => {
    serverOptions.Configure(context.Configuration.GetSection("Kestrel"))
    .Endpoint("HTTPS", listenOptions => { listenOptions.HttpsOptions.SslProtocols = SslProtocols.Tls12; });
});
string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
userFolder = Path.Combine(userFolder, ".aspnet");
userFolder = Path.Combine(userFolder, "https");
userFolder = Path.Combine(userFolder, "Nick.pxf");
builder.Configuration.GetSection("Kestrel:Endpoints:Https:Certificate:Path").Value = userFolder;

string kestrelPassword = builder.Configuration.GetValue<string>("KestrelPassword");
builder.Configuration.GetSection("Kestrel:Endpoints:Https:Certificate:Password").Value = kestrelPassword;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
