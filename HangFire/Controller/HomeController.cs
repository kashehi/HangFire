using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangFire.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class HomeController : ControllerBase
    {

        [Route("FireAndForget")]
        public string FireAndForget()
        {

            var jobId = BackgroundJob.Enqueue(methodCall: () => Console.WriteLine("fireAndForgetJob"));
            return $"JobId: { jobId}, FireAndForgetJob";
        }


        [Route("DelayedJob")]
        public string DelayedJob()
        {
            var jobId = BackgroundJob.Schedule(methodCall: () => Console.WriteLine("DelayedJob"), delay: TimeSpan.FromSeconds(60));
            return $"JobId: { jobId}, DelayedJob";
        }


        [Route("ContinouosJob")]
        public string ContinouosJob()
        {
            var parentJobId = BackgroundJob.Enqueue(methodCall: () => Console.WriteLine("fireAndForgetJob"));
            var jobId = BackgroundJob.ContinueJobWith(parentId: parentJobId, methodCall: () => Console.WriteLine("ContinouosJob"));
            return $"JobId: { jobId}, ContinouosJob";
        }


        [Route("RecurringJob")]
        public string RecurringJobs()
        {
            RecurringJob.AddOrUpdate(recurringJobId: "jobId", methodCall: () => Console.WriteLine("RecurringJob"), cronExpression: Cron.Weekly);
            return $"RecurringJobs";
        }

    }
}
