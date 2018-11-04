using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using StealthGirder.Infrastructure;
using StealthQuartz;

namespace StealthGirder
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
            AddQuartz(services, new BackHandle1("aaa"), new BackHandle2("123"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            UserBackHandle(app, scheduler);

            app.UseMvc();
        }
        void UserBackHandle(IApplicationBuilder app, IScheduler scheduler)
        {

            QuartzServicesUtilities.StartJob<BackgroundJob<IBackHandle>>(scheduler, "10 33 8 * * ?", "BackHandle1");
            QuartzServicesUtilities.StartJob<BackgroundJob<IBackHandle>>(scheduler, "40 33 8 * * ?", "BackHandle2");
        }

        void AddQuartz(IServiceCollection services, params IBackHandle[] backHandles)
        {
            foreach (var backHandle in backHandles)
            {
                services.AddTransient(pro =>
                {
                    return backHandle;
                });
            }

            services.AddSingleton(factory =>
            {
                Func<string, IBackHandle> accesor = key =>
                {
                    foreach (var backHandle in backHandles)
                    {
                        if (key == backHandle.GetType().Name)
                        {
                            return factory.GetService(backHandle.GetType()) as IBackHandle;
                        }
                    }
                    throw new ArgumentException($"Not Support key : {key}");
                };
                return accesor;
            });

            services.UseQuartz(typeof(BackgroundJob<IBackHandle>));



        }
    }
}
