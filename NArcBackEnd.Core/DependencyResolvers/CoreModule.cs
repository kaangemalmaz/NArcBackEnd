using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NArcBackEnd.Core.CrossCuttingConcerns.Caching;
using NArcBackEnd.Core.CrossCuttingConcerns.Caching.Microsoft;
using NArcBackEnd.Core.Utilities.IoC;

namespace NArcBackEnd.Core.DependencyResolvers
{
    //servis ekleme katmanı.
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>(); // injection
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Secured Aspect - 1 // core katmanında userin claimlerine ulaşmayı sağlar.
        }
    }
}
