using EmployeeManagement_BLL;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.CountryRepository;
using Repository.DepartmentRepository;
using Repository.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement_API
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
            services.AddDbContext<EmployeeManagement_DBContext>(options =>
          options.UseSqlServer(Configuration.GetSection("ConnectionString").Value));

            #region Dependency Injection Container
            services.AddDbContext<EmployeeManagement_DBContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped(typeof(DepartmentBLL));

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped(typeof(CountryBLL));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped(typeof(EmployeeBLL));
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagement_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagement_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseExceptionMiddlewareAPI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
