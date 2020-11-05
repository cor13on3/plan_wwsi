using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Services;
using Plan.Infrastructure.DB;
using Plan.Serwis.BazaDanych;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                // TODO: Obeznaæ te CORS jebane
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
            services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddDbContext<PlanContext>();
            services.AddSpaStaticFiles(c => c.RootPath = "admin");
            services.AddScoped<IWykladowcaService, WykladowcaService>();
            services.AddScoped<IGrupaService, GrupaService>();
            services.AddScoped<IKalendariumService, KalendariumService>();
            services.AddScoped<ISalaService, SalaService>();
            services.AddScoped<IPrzedmiotService, PrzedmiotService>();
            services.AddScoped<ILekcjaService, LekcjaService>();
            services.AddScoped<IUzytkownikService, UzytkownikService>();
            services.AddScoped<ISpecjalnoscService, SpecjalnoscService>();
            // TODO: Rozwazyæ sprytne DI ¿eby nie by³o tu referencji do infry
            services.AddScoped<IBazaDanych, EfBazaDanych>();
            services.AddScoped(typeof(IRepozytorium<>), typeof(EfRepozytorium<>));

            services.AddIdentity<Uzytkownik, IdentityRole>().AddEntityFrameworkStores<PlanContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // TODO: ZnaleŸæ lepsze rozwi¹zanie
            // OSTRO¯NIE BO PRZY UPDACIE BAZY MOG¥ ZNIKAÆ DANE ! ! !
            using (var context = new PlanContext())
            {
                context.Database.Migrate();
            }
            // ! ! !







            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            app.UseExceptionHandler("/error");
            //}
            app.UseCors("MyPolicy");

            app.UseRouting();

            app.UseAuthentication();
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
