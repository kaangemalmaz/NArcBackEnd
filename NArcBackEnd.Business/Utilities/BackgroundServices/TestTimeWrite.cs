using Microsoft.Extensions.Hosting;

namespace NArcBackEnd.Business.Utilities.BackgroundServices
{
    // arka görevde çalışan yapılar. Zamanlanmış görevlerdir. 
    // IHostedService den türer ve uygulama başladığı ve kapandığı anda işlem yapar.
    // https://learn.microsoft.com/tr-TR/troubleshoot/windows-client/system-management-components/task-scheduler-task-only-runs-in-background
    // https://learn.microsoft.com/tr-tr/azure/architecture/best-practices/background-jobs
    public class TestTimeWrite : IHostedService, IDisposable
    {
        private Timer timer;

        public void Dispose() //uygulama durarsa eğer timer sıfırlanır o yüzden bu eklenir.
        {
            timer = null;
        }

        public Task StartAsync(CancellationToken cancellationToken) // uygulama çalıştıktan sonra çalışmaya başlar
        {
            Console.WriteLine("Uygulama çalıştı");
            timer = new Timer(WriteTimerOnScreen, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) // uygulama durduktan sonra çalışır
        {
            Console.WriteLine("Uygulama kapandı");
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void WriteTimerOnScreen(object? state)
        {
            Console.WriteLine($"Time is {DateTime.Now.ToLongTimeString()}");
        }
    }
}
