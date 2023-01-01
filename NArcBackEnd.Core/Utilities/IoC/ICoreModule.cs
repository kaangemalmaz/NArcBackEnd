using Microsoft.Extensions.DependencyInjection;

namespace NArcBackEnd.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
