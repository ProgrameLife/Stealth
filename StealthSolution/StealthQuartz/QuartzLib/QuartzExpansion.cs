using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace StealthQuartz
{
    public static class QuartzExpansion
    {
        public static void AddBackHandle(this IServiceCollection services, params IBackHandle[] backHandles)
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


        public static void UserBackHandle(this IApplicationBuilder app, IScheduler scheduler,params QuartzEntity[] quartzEntities)
        {           
            foreach (var quartzEntitie in quartzEntities)
            {
                QuartzServicesUtilities.StartJob<BackgroundJob<IBackHandle>>(scheduler, quartzEntitie.CronExpression, quartzEntitie.Name);
            }
        }
    }
}
