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
        /// use SqlServer BackHandle
        /// </summary>
        /// <param name="app">application builder</param>
        /// <param name="scheduler">quartz scheduler</param>
        public static void UserSqlServerBackHandle(this IApplicationBuilder app, IScheduler scheduler)
        {
            var provider = app.ApplicationServices.GetService<IQuartzProvider>();
            app.UserBackHandle(scheduler, provider.GetQuartzSetting().ToArray());
        }
        /// <summary>
        /// di SqlServerprovider and backhandles
        /// </summary>
        /// <param name="services">service collection</param>
        public static void AddSqlServerBackHandle(this IServiceCollection services)
        {
            services.AddTransient<IQuartzProvider, SqlServerQuartzProvider>();
            services.AddBackHandle();
        }    
    }
}
