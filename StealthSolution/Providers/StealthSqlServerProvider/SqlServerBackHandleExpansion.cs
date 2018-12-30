using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SealthProvider;
using StealthBackHandle;
using StealthQuartz;

namespace StealthSqlServerProvider
{
    /// <summary>
    /// 
    /// </summary>
    public static class SqlServerBackHandleExpansion
    {
        /// <summary>
        /// use postgre BackHandle
        /// </summary>
        /// <param name="app">application builder</param>
        /// <param name="scheduler">quartz scheduler</param>
        public static void UserPostgreBackHandle(this IApplicationBuilder app, IScheduler scheduler)
        {
            var provider = app.ApplicationServices.GetService<IQuartzProvider>();
            app.UserBackHandle(scheduler, provider.GetQuartzSetting().ToArray());
        }
        /// <summary>
        /// di postgreprovider and backhandles
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="backHandles">back handles</param>
        public static void AddPostgreBackHandle(this IServiceCollection services)
        {
            services.AddScoped<IQuartzProvider, SqlServerQuartzProvider>();
            services.AddBackHandle();
        }
    }
}
