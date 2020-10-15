using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Services;
using Plan.Infrastructure.DB;
using Plan.Serwis.BazaDanych;

namespace Plan.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<PlanContext>();
            services.AddSpaStaticFiles(c => c.RootPath = "admin");
            services.AddScoped<IWykladowcaService, WykladowcaService>();
            services.AddScoped<IGrupaService, GrupaService>();
            services.AddScoped<IKalendariumService, KalendariumService>();
            services.AddScoped<ISalaService, SalaService>();
            services.AddScoped<IPrzedmiotService, PrzedmiotService>();
            services.AddScoped<ILekcjaService, LekcjaService>();
            // TODO: Rozwazyæ sprytne DI ¿eby nie by³o tu referencji do infry
            services.AddScoped<IBazaDanych, EfBazaDanych>();
            services.AddScoped(typeof(IRepozytorium<>), typeof(EfRepozytorium<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "admin";
            });
        }
    }
}
