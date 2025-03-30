using KnowCloud.Contract;
using KnowCloud.Filters;
using KnowCloud.Services;
using KnowCloud.Services.Contract;
using KnowCloud.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilterAttribute>(); // Agrega el filtro de excepción globalmente
});
builder.Services.AddHttpContextAccessor();

Utilities.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];

builder.Services.AddHttpClient<IAuthServices, AuthServices>();
builder.Services.AddTransient<IDataCloudAzure, DataCloudAzure>();
//builder.Services.AddScoped<IDataCloudAzure, UploadFileLocal>();

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

//podemos subir archivos a traves del services.
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // Tamaño máximo de archivo permitido (10 MB)
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Auth/Denied";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("RoleAdmin"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithRedirects("/Account/Denied");

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
