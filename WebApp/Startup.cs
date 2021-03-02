using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Data;
using WebApp.Services;


namespace WebApp
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
            services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AccountDb")));
           
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 5;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<AccountContext>();
           
            services.AddDbContext<StoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("StoreDb")));
           
            services.AddTransient<IStoreRepository, StoreRepo>();

            
            services.AddControllersWithViews()
                 .AddNewtonsoftJson(options =>
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
          
           
          
        }

        
        public  void Configure( IApplicationBuilder app, 
                                IWebHostEnvironment env, 
                                StoreContext context, 
                                IStoreRepository repo,
                                RoleManager<IdentityRole> rolemanager, 
                                UserManager<User> userManager )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();        
            app.UseAuthorization();

            if (context.Database.EnsureCreated())
            {
                DbInitializer.Initialize(rolemanager, userManager, repo);

            }



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }





     

    }

}
