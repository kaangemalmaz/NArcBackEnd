using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using NArcBackEnd.Core.CrossCuttingConcerns.Caching;
using NArcBackEnd.Core.Utilities.Interceptors;
using NArcBackEnd.Core.Utilities.IoC;

namespace NArcBackEnd.Core.Aspects.Caching
{
    // cache - 6
    public class RemoveCacheAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public RemoveCacheAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            //var aa = _pattern;
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
