using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Quartz;
using SealthProvider;
using StealthBackHandle;
using StealthBuildData;
using StealthEmailBackHandle;
using StealthFileBackHandle;
using StealthPostgreProvider;
using StealthSqlServerProvider;
using System.Globalization;
using System.Reflection;

namespace StealthGirder
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
            #region base mode
            //services.AddBackHandle(new BackHandle1("aaa"), new BackHandle2("123"));
            #endregion

            #region stealth provider          
            services.AddTransient<IEmailProvider, PostgreEmailProvider>();
            services.AddTransient<ISFTPProvider, PostgreSFTPProvider>();
            services.AddTransient<SealthProvider.IFileProvider, PostgreFileProvider>();
            services.AddTransient<IStealthStatuProvider, PostgreStealthStatuProvider>();

            //services.AddTransient<IEmailProvider, SqlServerEmailProvider>();
            //services.AddTransient<ISFTPProvider, SqlServerSFTPProvider>();
            //services.AddTransient<SealthProvider.IFileProvider, SqlServerFileProvider>();
            //services.AddTransient<IStealthStatuProvider, SqlServerStealthStatuProvider>();
            #endregion

            #region postgre mode               
            services.AddTransient<IBuildData, DemoBuildData>();
            services.AddTransient<EmailBackHandle>();
            services.AddTransient<FileBackHandle>();

             services.AddPostgreBackHandle();
            //services.AddSqlServerBackHandle();
            #endregion
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(
                    new EmbeddedFileProvider(typeof(StealthUI.Controllers.FileSettingsController).GetTypeInfo().Assembly));
            });

            services.AddLocalization(opt => opt.ResourcesPath = "Resources");
            services
                .AddMvc()
                .AddApplicationPart(typeof(StealthUI.Controllers.FileSettingsController).GetTypeInfo().Assembly)              
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region base mode
            //var quartzEntities = new List<QuartzEntity>();
            //quartzEntities.Add(new QuartzEntity { Name = "BackHandle1", CronExpression = "10 33 8 * * ?" });
            //quartzEntities.Add(new QuartzEntity { Name = "BackHandle2", CronExpression = "40 33 8 * * ?" });
            //app.UserBackHandle(scheduler, quartzEntities.ToArray());
            #endregion

            #region postgre mode
            app.UserPostgreBackHandle(scheduler);
            //app.UserSqlServerBackHandle(scheduler);
            #endregion
            var supportedCultures = new[]{           
                new CultureInfo("zh")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {                 
                DefaultRequestCulture = new RequestCulture(new CultureInfo("zh")),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(StealthUI.Controllers.FileSettingsController).GetTypeInfo().Assembly),

            });
            app.UseMvc();
        }
    }
 
}
