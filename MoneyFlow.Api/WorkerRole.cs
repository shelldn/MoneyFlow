using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace MoneyFlow.Api
{
    public class WorkerRole : RoleEntryPoint
    {
        private IDisposable _app;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly ManualResetEvent _ce = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("MoneyFlow.Api is running");

            try
            {
                RunAsync(_cts.Token).Wait();
            }
            finally
            {
                _ce.Set();
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            _app = WebApp.Start<Startup>(url: "http://+:82");

            return base.OnStart();
        }

        public override void OnStop()
        {
            _app?.Dispose();
            base.OnStop();
        }

        private static async Task RunAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000, ct);
            }
        }
    }
}
