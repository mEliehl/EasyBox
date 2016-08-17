using Application;
using Application.Interfaces;
using Domain.Repositories;
using Infra.RavenDB.Repositories;
using ReadModel;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using System.Reflection;

namespace CompositionRoot
{
    public class ApplicationBootstrap
    {
        public static Container Compose()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<IProductRepository, ProductRepository>();

            container.Register(typeof(ICommandHandler<>),
                new[] { typeof(ICommandHandler<>).GetTypeInfo().Assembly });

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionHandler<>));
            container.Register(typeof(IQueryHandler<,>),
                new[] { typeof(IQueryHandler<,>).GetTypeInfo().Assembly });

            return container;
        }
    }
}
