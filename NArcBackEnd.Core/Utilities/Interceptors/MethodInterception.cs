using Castle.DynamicProxy;

namespace NArcBackEnd.Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }

        public override void Intercept(IInvocation invocation) //IInvocation metodda ne var ne yoksa taşır. direk metodu yollar !
        {
            var isSuccess = true; //işlemin başlangıçta hata almadığını düşünerek başlatıyoruz.

            OnBefore(invocation);

            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);


        }

    }
}
