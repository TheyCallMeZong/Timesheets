using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
<<<<<<< HEAD
using Timesheets.Data.Ef;
using Timesheets.Data.Implementations;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Implementations;
using Timesheets.Domain.Interfaces;
using Timesheets.Infrastucture.Extensions;
=======
using Timesheets.Domain.Implementations;
using Timesheets.Domain.Interfaces;
>>>>>>> parent of 5f3582c (пофиксил недочеты)

namespace Timesheets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
<<<<<<< HEAD
            services.DbConfiguration(Configuration);
            services.AuthenticateConfiguration(Configuration);
            services.RepositoriesConfig();
            services.ManagerConfig();
            services.SwaggerConfiguration();
=======
            services.AddSingleton<IPersonManager, PersonManager>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Timesheets"
                });
            });

>>>>>>> parent of 5f3582c (пофиксил недочеты)
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Timesheets");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}