
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StealthQuartz
{
    /// <summary>
    /// quartz services utilities
    /// </summary>
    public static class QuartzServicesUtilities
    {
        public async static void StartJob<TJob>(IScheduler scheduler, string cronExpression, string handleName)
            where TJob : IJob
        {
            var jobName = Guid.NewGuid().ToString();
            var job = JobBuilder.Create<TJob>()
                .UsingJobData("cronexpression", cronExpression)
                .UsingJobData("handlename", handleName)
                .WithIdentity(jobName)
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .WithCronSchedule(cronExpression)
                .StartNow()
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
