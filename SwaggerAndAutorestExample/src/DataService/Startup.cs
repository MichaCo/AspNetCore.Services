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
    /// <summary>
    /// The service startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The startup gets the <see cref="IHostingEnvironment"/> injected.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Keeps the appsettings configuration for later use.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("doc", new Info() { Title = "DataService" });

                var fileName = this.GetType().GetTypeInfo().Module.Name.Replace(".dll", ".xml").Replace(".exe", ".xml");
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));

                var modelFileName = typeof(Shared.BlogPostModel).GetTypeInfo().Module.Name.Replace(".dll", ".xml");
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, modelFileName));
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The app builder.</param>
        /// <param name="loggerFactory">Our logger factory.</param>
        /// <param name="env">The <see cref="IHostingEnvironment"/>.</param>
        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IHostingEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/doc/swagger.json", "DataService API");
                });
            }

            app.UseMvc();
        }
    }
}