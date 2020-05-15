using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PathFinder.Api.Data;
using PathFinder.Api.Services;


namespace PathFinder.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PathFinderContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("PathFinderContext"));
            services.AddDbContext<PathFinderContext>();

            services.AddControllers();
            services.AddSingleton(typeof(IPathFinder), typeof(PathFinder));
            services.AddSingleton<IPathFinderService>((sp) =>
            {
                using var scope = sp.CreateScope();
                return new PathFinderService(scope.ServiceProvider.GetService<IPathFinder>(), optionsBuilder.Options);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
