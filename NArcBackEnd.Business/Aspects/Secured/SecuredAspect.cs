using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NArcBackEnd.Core.Extensions;
using NArcBackEnd.Core.Utilities.Interceptors;
using NArcBackEnd.Core.Utilities.IoC;
using System.Security.Claims;

namespace NArcBackEnd.Business.Aspects.Secured
{
    // Secured Aspect - 3
    public class SecuredAspect : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; // istek ile ilgili tüm içeriğe ulaşmanı sağlar.

        public SecuredAspect() // Secured Aspect - 5 // sadece giriş kontrolü yetki kontrolü olmadan!
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public SecuredAspect(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        
        // çözümlenme program.cs ve direk olarak bilgiler claim yapısına yazılır.
        protected override void OnBefore(IInvocation invocation)
        {

            if (_roles != null)
            {
                // istek düştüğü sırada içerik ile ulaşıyoruz. token çözümleniyor ve oradan kişinin rolleri bulunuyor.
                // var aa = _httpContextAccessor.HttpContext.User.Claims(claimType: ClaimTypes.Role); //aynı metoddan bu şekildede alabilirsin 2.ye gerek yok!
                // var bb = _httpContextAccessor.HttpContext.Request;
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
                foreach (var role in _roles)
                {
                    if (roleClaims.Contains(role))
                    {
                        return;
                    }
                }
                throw new Exception("İşlem için yetkili değilsiniz");
            }
            else // Secured Aspect - 5
            {
                // sadece giriş kontrolü yetki kontrolü olmadan!
                var claim = _httpContextAccessor.HttpContext.User.Claims; // token ile giriş yaptığında rolleri kullanmasan bile tokena atılan bilgiler dolu geliyor.
                if (claim.Count() > 0)
                {
                    return;
                }
                throw new Exception("Bu işlem için giriş yapmanız gerekmektedir.");
            }
        }
    }
}
