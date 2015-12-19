using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SportyWarsaw.Domain;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Infrastructure;
using SportyWarsaw.WebApi.Providers;
using System.Reflection;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(SportyWarsaw.WebApi.Startup))]
namespace SportyWarsaw.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();

            BuildContainer(app, builder);

            WebApiConfig.Register(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            ConfigureAuth(app);
            app.UseWebApi(config);
        }

        private static void BuildContainer(IAppBuilder app, ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyModules(typeof(SportyWarsawContext).Assembly);

            builder.RegisterType<ApplicationUserStore>()
                .As<IUserStore<User>>()
                .InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>()
                .AsSelf()
                .InstancePerRequest();

            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider())
                .InstancePerRequest();

            builder.RegisterType<ApplicationOAuthProvider>()
                .AsImplementedInterfaces()
                .WithParameter("publicClientId", "self");
        }
    }
}