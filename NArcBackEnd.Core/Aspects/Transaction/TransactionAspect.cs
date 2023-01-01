using Castle.DynamicProxy;
using NArcBackEnd.Core.Utilities.Interceptors;
using System.Transactions;

namespace NArcBackEnd.Core.Aspects.Transaction
{
    public class TransactionAspect : MethodInterception
    {
        // dikkat override işlemi burada direk ana metodda yapılıyor.
        // çünkü bu işlemin gerçekleştiği esnada meydana gelir tamamında öncesi sonrasında falan değil!
        public override void Intercept(IInvocation invocation) 
        {
            using (TransactionScope transactionScope = new TransactionScope()) //asp.net core ile geldi!
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete(); // işlemleri tamamla!
                }
                catch (Exception)
                {
                    transactionScope.Dispose(); // yaptığım tüm işlemleri geri al
                    throw;
                }
            }
        }
    }
}
