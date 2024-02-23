using DAOs;
using Microsoft.EntityFrameworkCore;
using Repositories.ServiceExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SdcrpDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("SdcrpConnection"));
	options.UseNpgsql(builder => builder.MigrationsAssembly("SelfDrivingCarRentalPlatform"));
}, ServiceLifetime.Singleton);
builder.Services.InjectServices(); // add service for DI

//add cookie authentication
builder.Services.AddAuthentication("LoginCookieAuth")
	.AddCookie("LoginCookieAuth", options =>
	{
		options.Cookie.Name = "LoginCookieAuth";
		options.LoginPath = "/Account/Login";
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();