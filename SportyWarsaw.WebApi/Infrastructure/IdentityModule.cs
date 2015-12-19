using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Providers;
using System.Web;
using Module = Autofac.Module;

namespace SportyWarsaw.WebApi.Infrastructure
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserStore>()
                .As<IUserStore<User>>()
                .InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>()
                .AsSelf()
                .InstancePerRequest();

            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();

            builder.RegisterType<ApplicationOAuthProvider>()
                .AsImplementedInterfaces()
                .WithParameter("publicClientId", "self");
        }
    }
}