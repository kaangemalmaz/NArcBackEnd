using Microsoft.Extensions.DependencyInjection;
using NArcBackEnd.Core.CrossCuttingConcerns.Caching;
using NArcBackEnd.Core.CrossCuttingConcerns.Caching.Microsoft;
using NArcBackEnd.Core.Utilities.IoC;

namespace NArcBackEnd.Core.DependencyResolvers
{
    // cache - 4
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>(); // injection
        }
    }
}
