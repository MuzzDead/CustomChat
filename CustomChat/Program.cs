using CustomChat.Data;
using CustomChat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomChat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("default");

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddSignalR();

			builder.Services.AddDbContext<AppDbContext>(
				option => option.UseSqlServer(connectionString));

			builder.Services.AddIdentity<AppUser, IdentityRole>(
				options =>
				{
					options.Password.RequiredUniqueChars = 0;
					options.Password.RequireUppercase = false;
					options.Password.RequiredLength = 8;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireLowercase = false;
				}
				).
				AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


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

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapHub<ChatHub>("/Chat");

			app.Run();
		}
	}
}