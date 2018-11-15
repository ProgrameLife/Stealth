﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using StealthQuartz;

namespace StealthPostgreProvider
{
    /// <summary>
    /// 
    /// </summary>
    public static class PostgreBackHandleExtension
    {
        /// <summary>
        /// use postgre BackHandle
        /// </summary>
        /// <param name="app">application builder</param>
        /// <param name="scheduler">quartz scheduler</param>
        public static void UserPostgreBackHandle(this IApplicationBuilder app, IScheduler scheduler)
        {
            var provider = app.ApplicationServices.GetService<IDBProvider>();
            app.UserBackHandle(scheduler, provider.GetQuartzEntity().ToArray());
        }
        /// <summary>
        /// di postgreprovider and backhandles
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="backHandles">back handles</param>
        public static void AddPostgreBackHandle(this IServiceCollection services, params IBackHandle[] backHandles)
        {
            services.AddTransient<IDBProvider, PostgreProvider>();
            services.AddBackHandle(backHandles);
        }
    }
}
