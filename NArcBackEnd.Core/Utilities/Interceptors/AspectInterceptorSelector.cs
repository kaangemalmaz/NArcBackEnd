using Castle.DynamicProxy;
using System.Reflection;

namespace NArcBackEnd.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            // bu interceptor üzerindeki kaçtane attribute varsa hepsini yakalamanı sağlar.
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            classAttributes.AddRange(methodAttributes);
            return classAttributes.ToArray(); 
        }
    }
}
