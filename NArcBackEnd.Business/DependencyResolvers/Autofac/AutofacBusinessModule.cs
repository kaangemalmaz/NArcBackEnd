﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Business.Concrete;
using NArcBackEnd.Core.Utilities.Interceptors;
using NArcBackEnd.Core.Utilities.Security.JWT;
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

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<EmailParameterManager>().As<IEmailParameterService>();
            builder.RegisterType<EfEmailParameterDal>().As<IEmailParameterDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            //jwt
            builder.RegisterType<TokenHandler>().As<ITokenHandler>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // çalışan assembly yi al.

            //buna bir bak sen yinede!
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
                new Castle.DynamicProxy.ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
