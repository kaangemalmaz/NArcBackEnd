using Microsoft.Extensions.DependencyInjection;

namespace NArcBackEnd.Core.Utilities.IoC
{
    // buradaki amaç program.cs deki servis yapısına ulaşayım ona ekleme yapayım ve oraya geri döndürüyüm. Tüm projelerde kullanacağım yapılar için.
    // autofac ile aynı mantığa gelecek ama tüm projelerde ortak kullanılacak yapıları içerecek.
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            // alttaki blog console veya diğer uygulamalarda built in injection olmayan uygulamalarda servis sağlayıcı yani bu aslında sunu sağlıyor.
            // ben senden IService istersen sen bana Service ver anlamına geliyor.
            // https://www.gencayyildiz.com/blog/net-core-console-applicationda-dependency-injection-kullanimi/
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
