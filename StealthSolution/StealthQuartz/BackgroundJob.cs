
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace StealthQuartz
{
    /// <summary>
    /// back task
    /// </summary>
    public class BackgroundJob<T> : IJob where T : IBackHandle
    {
        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<BackgroundJob<T>> _logger;
        /// <summary>
        /// muntil backhandel
        /// </summary>
        readonly Func<string, IBackHandle> _serviceAccessore;

        public BackgroundJob(ILogger<BackgroundJob<T>> logger, Func<string, IBackHandle> serviceAccessore)
        {
            _serviceAccessore = serviceAccessore;
            _logger = logger;
        }
        /// <summary>
        /// call back method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var result = _serviceAccessore(context.JobDetail.JobDataMap.GetString("handlename")).Handle();
                _logger.LogInformation($"IBackHandle.Handle call return {result}");
            }
            catch (Exception exc)
            {
                LogException(exc);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// log all exception
        /// </summary>
        /// <param name="exc"></param>
        void LogException(Exception exc)
        {
            _logger.LogCritical(exc, exc.Message);
            if (exc.InnerException != null)
            {
                LogException(exc.InnerException);
            }
        }
    }
}
