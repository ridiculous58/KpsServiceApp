using Autofac;
using Autofac.Extras.DynamicProxy;
using DataAccess.EntityFramework.Concrete;
using DataAccess.Interfaces;
using Infrastructure.Utilities.Interceptors;
using Infrastructure.Utilities.Security.Jwt;
using Service.AuthService;
using Service.UserControlService;
using Service.UserControlService.KpsServiceAdapter;
using Service.UserControlService.KpsServiceAdapter.Helpers;
using Service.UserService;

namespace Service.DependencyResolvers.Autofac
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Service.AuthService.AuthService>().As<IAuthService>();
            
            builder.RegisterType<UserDal>().As<IUserDal>();
            builder.RegisterType<UserOperationClaimDal>().As<IUserOperationClaimDal>();
            builder.RegisterType<OperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<Service.UserService.UserService>().As<IUserService>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<KpsServiceAdapter>().As<IUserControlService>();
            builder.RegisterType(typeof(KpsRequestHelper));

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(
                new Castle.DynamicProxy.ProxyGenerationOptions()
                { Selector = new AspectInterceptorSelector() }).SingleInstance();
        }
    }
}
