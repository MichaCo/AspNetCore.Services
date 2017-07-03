using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace DataService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("doc", new Info() { Title = "DataService" });

                var fileName = this.GetType().GetTypeInfo().Module.Name.Replace(".dll", ".xml");
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));

                var modelFileName = typeof(Shared.BlogPostModel).GetTypeInfo().Module.Name.Replace(".dll", ".xml");
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, modelFileName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IHostingEnvironment env)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/doc/swagger.json", "DataService API");
                });
            }

            app.UseMvc();
        }
    }
}