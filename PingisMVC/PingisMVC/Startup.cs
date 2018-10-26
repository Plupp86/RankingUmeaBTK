using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UmeaBTKRanking.Models;
using UmeaBTKRanking.Models.Entities;

namespace UmeaBTKRanking
{
	public class Startup
	{
		IConfiguration conf;
		public Startup(IConfiguration conf)
		{
			this.conf = conf;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			//var connString = conf.GetConnectionString("PingisDB");
			var connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MiniProject_v3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(o => o.LoginPath = "/Admin/Login");

			services.AddDbContext<PingisContext>(o => o.UseSqlServer(connString));  //Måste ligga före identityContexten

			services.AddDbContext<IdentityDbContext>(o =>
				o.UseSqlServer(connString));

			services.AddTransient<Repository>();
			services.AddTransient<RepositoryHandler>();
			services.AddTransient<AccountRepository>();
			services.AddTransient<RepositoryHandlerApi>();

			services.AddIdentity<IdentityUser, IdentityRole>(o =>
			{
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequireDigit = false;
				o.Password.RequireUppercase = false;
				o.Password.RequiredLength = 6;
			})
				.AddEntityFrameworkStores<IdentityDbContext>()
				.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(
				o => o.LoginPath = "/Admin/Login");

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			//if (env.IsDevelopment())
			//{
				app.UseDeveloperExceptionPage();
			//}
			//else
			//{
			//	app.UseExceptionHandler("/Error/ServerError");
			//	app.UseStatusCodePagesWithRedirects("/Error/HttpError/{0}");
			//}

			app.UseAuthentication();
			app.UseMvcWithDefaultRoute();
			app.UseStaticFiles();
		}
	}
}
