
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
    public class BackgroundJob : IJob
    {
        readonly ILogger<BackgroundJob> _logger;
        readonly  Action<bool> _action;

        public BackgroundJob(ILogger<BackgroundJob> logger, Action<bool> action)
        {
            _action = action;
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                if (context.JobDetail is JobDetailImpl)
                {
                    var names = (context.JobDetail as Quartz.Impl.JobDetailImpl)?.Name.Split('_');
                    var method = names.Length > 1 ? names[1] : "";
                    if (!string.IsNullOrEmpty(method))
                    {
                        //todo 这里用什么方法实现自动调用？？？？？？
                        //_action()
                        //var methodInfo = _type.GetMethod(method);
                        //var result = methodInfo.Invoke(_backgroundRepository, new object[0]);
                        //var runResult = false;
                        ////成功执行
                        //if (bool.TryParse(result.ToString(), out runResult) && runResult)
                        //{
                        //    _logger.LogInformation($"BackgroundJob.Execute成功，方法：{method}");
                        //}
                        //else//不成功执行
                        //{
                        //    _logger.LogInformation($"BackgroundJob.Execute失败，方法：{method}");
                        //}
                    }
                }
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
