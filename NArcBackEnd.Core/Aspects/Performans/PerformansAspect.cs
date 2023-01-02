using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using NArcBackEnd.Core.Utilities.Interceptors;
using NArcBackEnd.Core.Utilities.IoC;
using System.Diagnostics;

namespace NArcBackEnd.Core.Aspects.Performans
{
    public class PerformansAspect : MethodInterception
    {
        private int _interval; // programa kaç sn sonrasında bilgi vermesi gerektiğini söylecek. Yani performans problemi ne zaman başlıyor.
        private Stopwatch _stopWatch;

        public PerformansAspect()
        {
            _interval = 3;
            _stopWatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        public PerformansAspect(int interval)
        {
            _interval = interval;
            _stopWatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopWatch.Start(); //zamanlayıcıyı çalıştır.
        }

        protected override void OnAfter(IInvocation invocation)
        {
            _stopWatch.Stop(); //zamanlayıcı durdur.
            double seconds = _stopWatch.Elapsed.TotalSeconds;
            if (seconds > _interval)
            {
                //mail at kodu
                //db kaydet kodu
                Debug.WriteLine($"Performans Raporu : {invocation.Method.ReflectedType.FullName}.{invocation.Method.Name} ==> {seconds}");
                throw new Exception("Performans limiti aşıldı!");
            }
            _stopWatch.Reset(); //sıfırla
        }


    }
}
