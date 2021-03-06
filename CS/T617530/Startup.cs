using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace T617530 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            
            services.AddMvc();
            services.AddDevExpressControls(options => {
                options.Bootstrap(bootstrapOptions => {
                    bootstrapOptions.IconSet = BootstrapIconSet.Embedded;
                    bootstrapOptions.Mode = BootstrapMode.Bootstrap4;
                });
                options.Resources = ResourcesType.DevExtreme;
            });
            // Sample data context registration
            services.AddDbContext<T617530.Models.NorthwindContext>(options => {
                options.UseSqlite(Configuration.GetConnectionString("NorthwindConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseDevExpressControls();
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                            }
            else
                app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();
                        app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Sample}/{action=GridView}/{id?}");
            });
        }
    }
}