
using Quartz;
using System;

namespace StealthQuartz
{
    /// <summary>
    /// quartz services utilities
    /// </summary>
    public static class QuartzServicesUtilities
    {
        public async static void StartJob<TJob>(IScheduler scheduler, string cronExpression, string typeName, string keyName)
            where TJob : IJob
        {
            var jobName = Guid.NewGuid().ToString();
            var job = JobBuilder.Create<TJob>()
                .UsingJobData("cronexpression", cronExpression)
                .UsingJobData("typename", typeName)
                .UsingJobData("keyname", keyName)
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
