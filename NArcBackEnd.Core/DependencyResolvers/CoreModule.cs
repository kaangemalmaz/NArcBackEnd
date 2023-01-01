using Microsoft.Extensions.DependencyInjection;
using NArcBackEnd.Core.Utilities.IoC;

namespace NArcBackEnd.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}
