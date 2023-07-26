using FluentValidation;
using Hvex.Data.Context;
using Hvex.Data.Repository;
using Hvex.Domain.Interface.Repository;
using Hvex.Domain.Interface.Services;
using Hvex.Domain.Services;
using Hvex.Exception.ExceptionBase;
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hvex {
    public class Startup {

            private string[] args;

            public Startup(IConfiguration configuration) {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            // This method gets called by the runtime. Use this method to add services to the container.     
            [Obsolete]
            public void ConfigureServices(IServiceCollection services) {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(opt => opt.Filters.Add(typeof(FilterExcepetion)));

            services.AddScoped<ITestRepo, TestRepo>();
            services.AddScoped<IReportRepo, ReportRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ITransformerRepo, TransformerRepo>();

            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransformerService, TransformerService>();

            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                   .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                       Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                   .AddNewtonsoftJson(opt => opt.SerializerSettings.NullValueHandling =
                       Newtonsoft.Json.NullValueHandling.Ignore)
                   .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddControllers().AddJsonOptions(x =>
     x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

                services.AddCors();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiHvex.API", Version = "v1" });
                });
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
                if (env.IsDevelopment()) {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HvexApi.API v1"));
                }

                app.UseAuthentication();

                //app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthorization();
            app.UseCors(cors => cors.AllowAnyHeader()
            .AllowAnyMethod().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
