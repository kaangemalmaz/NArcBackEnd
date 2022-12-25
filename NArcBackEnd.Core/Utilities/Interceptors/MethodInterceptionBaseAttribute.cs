using Castle.DynamicProxy;

namespace NArcBackEnd.Core.Utilities.Interceptors
{
    // ben bir attribute oluşturmak istiyorum bunu class metod kullanabilir ve birden çok kez kullanabilir ve inherit edilebilir.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method , AllowMultiple = true,  Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        // intercept e tüm metodun toptan gönderilmesi gerekiyor. yani neye koyuyorsan onun herşeyini yollaman gerekir. Onu da IInvocation ile sağlıyorsun. Bu da castle.Core paketinden geliyor.
        public virtual void Intercept(IInvocation invocation)
        {
            
        }
    }
}
