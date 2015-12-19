using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SportyWarsaw.Domain;
using System.Reflection;
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
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider())
                .InstancePerRequest();
        }
    }
}