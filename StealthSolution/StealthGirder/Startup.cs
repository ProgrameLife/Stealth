﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using StealthGirder.Infrastructure;
using StealthQuartz;
using System.Collections.Generic;

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
            services.AddBackHandle(new BackHandle1("aaa"), new BackHandle2("123"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var quartzEntities = new List<QuartzEntity>();
            quartzEntities.Add(new QuartzEntity { Name = "BackHandle1", CronExpression = "10 33 8 * * ?" });
            quartzEntities.Add(new QuartzEntity { Name = "BackHandle2", CronExpression = "40 33 8 * * ?" });

            app.UserBackHandle(scheduler, quartzEntities.ToArray());

            app.UseMvc();
        }




    }
}
