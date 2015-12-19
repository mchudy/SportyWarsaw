using Autofac;

namespace SportyWarsaw.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SportyWarsawContext>()
                .AsSelf()
                .InstancePerRequest();
        }
    }
}
