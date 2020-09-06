using EntityFrameworkInMemoryAPI.Data.Context;
using EntityFrameworkInMemoryAPI.Data.Interfaces;
using EntityFrameworkInMemoryAPI.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace EntityFrameworkInMemoryAPI
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
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("Customers.db"));

            services.AddControllers();

            #region Swagger

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "API Artigo sobre REFIT",
                        Version = "v1",
                        Description = "API feita para consumo em artigo sobre REFIT",
                        Contact = new OpenApiContact
                        {
                            Name = "Lucas Eschechola",
                            Url = new Uri("https://www.linkedin.com/in/lucas-eschechola-769179166/")
                        }
                    });
            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            #region Swagger

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API REFIT - Lucas Eschechola " + DateTime.Now.ToString("yyyy"));
            });

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
