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
            AddQuartz(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            QuartzServicesUtilities.StartJob<BackgroundJob<IBackHandle>>(scheduler, "10 33 8 * * ?", "BackHandle1");

            QuartzServicesUtilities.StartJob<BackgroundJob<IBackHandle>>(scheduler, "40 33 8 * * ?", "BackHandle2");
            app.UseMvc();
        }

        void AddQuartz(IServiceCollection services)
        {
            services.AddTransient(pro =>
            {
                return new BackHandle1("abc");
            });
            services.AddTransient(pro =>
            {
                return new BackHandle2("abc");
            });
            services.AddSingleton(factory =>
            {
                Func<string, IBackHandle> accesor = key =>
                {
                    switch (key)
                    {
                        case "BackHandle1":
                            return factory.GetService<BackHandle1>();
                        case "BackHandle2":
                            return factory.GetService<BackHandle2>();
                        default:
                            throw new ArgumentException($"Not Support key : {key}");
                    }
                };
                return accesor;
            });

            services.UseQuartz(typeof(BackgroundJob<IBackHandle>));
        }
    }
}
