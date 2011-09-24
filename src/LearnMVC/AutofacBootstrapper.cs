using Autofac;
using Autofac.Integration.Mvc;

namespace LearnMVC
{
    public static class AutofacBootstrapper
    {
        public static IContainer Configure()
        {
            var mainAssembly = typeof(MvcApplication).Assembly;
            var builder = new ContainerBuilder();
            builder.RegisterControllers(mainAssembly);
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterAssemblyTypes(mainAssembly)
                .AsImplementedInterfaces();

            var container = builder.Build();
            return container;
        }
    }
}