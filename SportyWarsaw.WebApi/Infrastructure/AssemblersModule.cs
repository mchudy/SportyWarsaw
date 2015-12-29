using Autofac;

namespace SportyWarsaw.WebApi.Infrastructure
{
    public class AssemblersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AssemblersModule).Assembly)
               .Where(type => type.Name.EndsWith("Assembler"))
               .AsImplementedInterfaces()
               .SingleInstance();
        }
    }
}