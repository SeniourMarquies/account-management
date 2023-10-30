using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using nop.gg.Entities;

namespace nop.gg
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<DatabaseContext>(
				options =>
				{
					options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
					//opts.UseLazyLoadingProxies();

				}
				);
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(
				options =>
				{
					options.Cookie.Name = ".Nop.gg.auth";
					options.ExpireTimeSpan = TimeSpan.FromDays(3);
					options.SlidingExpiration = false;
					options.LoginPath = "/Account/Login";
					options.LogoutPath = "/Account/Logout";
					options.AccessDeniedPath = "/Home/AccessDenied";
				}
				);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}