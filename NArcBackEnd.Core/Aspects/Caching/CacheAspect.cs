using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using NArcBackEnd.Core.CrossCuttingConcerns.Caching;
using NArcBackEnd.Core.Utilities.Interceptors;
using NArcBackEnd.Core.Utilities.IoC;

namespace NArcBackEnd.Core.Aspects.Caching
{
    // cache - 3
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            //var aa = invocation.Method.ReflectedType.FullName;
            //var aa1 = invocation.Method;
            //var bb = invocation.Method.Name;
            // ReflectedType => yansıyan tipi yani onun referans tutucusu ondan yansıyor.

            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // "NArcBackEnd.Business.Abstract.IUserService.GetList"
            var arguments = invocation.Arguments.ToList(); // herhangi bir parametre almıyor.
            var key = $"{methodName}({string.Join(",", arguments.Select(p => p.ToString() ?? "<Null>"))})"; // "NArcBackEnd.Business.Abstract.IUserService.GetList()" //parametre olsa oda eklenecekti.
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);

        }
    }
}
