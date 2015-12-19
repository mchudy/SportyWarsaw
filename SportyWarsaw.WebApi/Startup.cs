using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using SportyWarsaw.Domain;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(SportyWarsaw.WebApi.Startup))]
namespace SportyWarsaw.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyModules(typeof(SportyWarsawContext).Assembly);

            WebApiConfig.Register(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}