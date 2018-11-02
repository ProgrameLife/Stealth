
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;

using System;
using System.Threading.Tasks;

namespace StealthQuartz
{
    /// <summary>
    /// 后台任务
    /// </summary>
    public class BackgroundJob<T> : IJob where T : IBackHandle
    {
        readonly ILogger<BackgroundJob<T>> _logger;
        readonly IBackHandle _handle;

        readonly Func<string, IBackHandle> _serviceAccessore;



        public BackgroundJob(ILogger<BackgroundJob<T>> logger, Func<string, IBackHandle> serviceAccessore)
        {
            _serviceAccessore = serviceAccessore;
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                //todo 参数没有处理
                _serviceAccessore(context.JobDetail.JobDataMap.GetString("handlename")).Handle();
            }
            catch (Exception exc)
            {
                _logger.LogCritical($"{DateTime.Now}:{exc.Message}");
                if (exc.InnerException != null)
                {
                    _logger.LogCritical($"     {DateTime.Now}:{exc.InnerException.Message}");
                }
            }
            return Task.CompletedTask;
        }
    }
}
