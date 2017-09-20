using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using System;
using Settings.Common.Interfaces;
using Settings.DataAccess;
using Settings.Services;

namespace Settings
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

        //Todo: Need to add the elastic logger back in once I figure out where I'm going with it. 
        public Logger GetLogger()
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            var url = "http://localhost:9200/";
            var elasticSearchSinkOptions = new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(url)) { AutoRegisterTemplate = true };
            elasticSearchSinkOptions.IndexFormat = "cupsim-{0:yyyy.MM.dd}";
           
            return new LoggerConfiguration()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("ApplicationName", "Cascading Settings Manager")
                .MinimumLevel.Is(Serilog.Events.LogEventLevel.Debug)
                .WriteTo.LiterateConsole()
                .WriteTo.ColoredConsole(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
                //.WriteTo.Elasticsearch(elasticSearchSinkOptions)
                .WriteTo.RollingFile("Application-{Date}-" + System.Diagnostics.Process.GetCurrentProcess().Id + ".log")
                .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });


            services.AddMvc();

            services.AddDbContext<SettingsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ISettingsProcessor, SettingsProcessor>();
            services.AddTransient<ISettingsDbContext, SettingsDbContext>();
            services.AddTransient<Queries>();
            services.AddTransient<HierarchyHelper>();
            services.AddSingleton(GetLogger());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, SettingsDbContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Settings}/{action=GetAllSettings}");
            });

            DbInitializer.Initialize(context);
        }
    }
}
