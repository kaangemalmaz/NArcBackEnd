using Autofac;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Business.Concrete;
using NArcBackEnd.DataAccess.Abstract;
using NArcBackEnd.DataAccess.Concrete.EntityFramework;

namespace NArcBackEnd.Business.DependencyResolvers.Autofac
{
    // sadece autofac yapısı kurulur.
    public class AutofacBusinessModule : Module
    {
        // container builder başlangıçta configure edilebildiği için o alınır.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
        }
    }
}
