using PersonalWebsite.Authorization;
using PersonalWebsite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalWebsite
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));
      services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

      services.AddMvc(config =>
      {
              // using Microsoft.AspNetCore.Mvc.Authorization;
              // using Microsoft.AspNetCore.Authorization;
              var policy = new AuthorizationPolicyBuilder()
                               .RequireAuthenticatedUser()
                               .Build();
        config.Filters.Add(new AuthorizeFilter(policy));
      })
         .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      // Authorization handlers.
      services.AddScoped<IAuthorizationHandler,
                            CommentIsOwnerAuthorizationHandler>();

      services.AddSingleton<IAuthorizationHandler,
                            CommentAdministratorsAuthorizationHandler>();

      services.AddSingleton<IAuthorizationHandler,
                            CommentManagerAuthorizationHandler>();

      services.AddScoped<IAuthorizationHandler,
                            ProjectIsOwnerAuthorizationHandler>();

      services.AddSingleton<IAuthorizationHandler,
                            ProjectAdministratorsAuthorizationHandler>();

      services.AddSingleton<IAuthorizationHandler,
                            ProjectManagerAuthorizationHandler>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
