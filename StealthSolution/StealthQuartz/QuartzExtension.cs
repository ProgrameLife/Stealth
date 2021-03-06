﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SealthModel;
using StealthBackHandle;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace StealthQuartz
{
    /// <summary>
    /// Quartz Expansion in asp.net core
    /// </summary>
    public static class QuartzExtension
    {
        /// <summary>
        /// DI back handle class
        /// </summary>
        /// <param name="services"></param>
        /// <param name="backHandles"></param>
        public static void AddBackHandle(this IServiceCollection services)
        {
            services.AddSingleton(factory =>
            {
                Func<string, IBackHandle> accesor = key =>
                {
                    foreach (var service in services)
                    {
                        if (service.ImplementationType?.Name == key)
                        {
                            var backHandle = factory.GetService(service.ImplementationType);
                            if(backHandle!=null)
                            {
                                return backHandle as IBackHandle;
                            }                        
                        }
                    }                   
                    throw new ArgumentException($"Not Support key : {key}");
                };
                return accesor;
            });
            var jobType = typeof(BackgroundJob<IBackHandle>);
            services.AddSingleton<IJobFactory, QuartzJonFactory>();
            var serviceDescriptor = new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton);
            services.Add(serviceDescriptor);
            services.AddSingleton(provider =>
            {
                var props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                var schedulerFactory = new StdSchedulerFactory(props);
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });
        }


        /// <summary>
        /// user middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="scheduler"></param>
        /// <param name="quartzEntities"></param>
        public static void UserBackHandle(this IApplicationBuilder app, IScheduler scheduler, params QuartzSetting[] quartzEntities)
        {
            foreach (var quartzEntitie in quartzEntities)
            {
                QuartzServicesUtilities.StartJob<BackgroundJob<IBackHandle>>(scheduler, quartzEntitie.CronExpression, quartzEntitie.TypeName, quartzEntitie.KeyName);
            }
        }
    }
}
