using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SealthProvider;
using StealthBackHandle;
using StealthBuildData;
using StealthEmailBackHandle;
using StealthFileBackHandle;
using StealthPostgreProvider;

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
            #endregion

            #region postgre mode               
            services.AddTransient<IBuildData, DemoBuildData>();
            services.AddTransient<EmailBackHandle>();
            services.AddTransient<FileBackHandle>();
            services.AddPostgreBackHandle();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            #endregion
          
            app.UseMvc();
        }




    }
    class BackHandle1 : IBackHandle
    {
        public bool Handle(string keyName)
        {
            throw new System.NotImplementedException();
        }
    }
}
