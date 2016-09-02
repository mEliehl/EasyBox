using Application;
using Application.Interfaces;
using Domain.Repositories;
using ReadModel;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CompositionRoot
{
    public class CompositionRootBootstrap
    {
        private IList<Func<Container, Container>> steps =
        new List<Func<Container, Container>>();

        private CompositionRootBootstrap()
        {
            steps.Clear();
        }

        private CompositionRootBootstrap UseCommands()
        {
            steps.Add((container) => {
                container.Register(typeof(ICommandHandler<>),
                new[] { typeof(ICommandHandler<>).GetTypeInfo().Assembly });
                container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionHandler<>));                

                return container;
            });
            return this;
        }

        private CompositionRootBootstrap UseQueries()
        {
            steps.Add((container) => {
                container.Register(typeof(IQueryHandler<,>),
                    new[] { typeof(IQueryHandler<,>).GetTypeInfo().Assembly });

                return container;
            });
            return this;
        }

        private CompositionRootBootstrap UseRavenDB()
        {
            steps.Add((container) => {
                container.Register<IUnitOfWork, Infra.RavenDB.Repositories.UnitOfWork>(Lifestyle.Scoped);
                container.Register<IProductRepository, Infra.RavenDB.Repositories.ProductRepository>();

                return container;
            });
            return this;
        }

        private CompositionRootBootstrap UseInMemoryDB()
        {
            steps.Add((container) => {
                container.Register<IUnitOfWork, Infra.InMemoryDB.Repositories.UnitOfWork>(Lifestyle.Scoped);
                container.Register<IProductRepository, Infra.InMemoryDB.Repositories.ProductRepository>();

                return container;
            });
            return this;
        }

        private Container Build()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            foreach (var step in steps)
                step(container);
            
            return container;
        }

        public static Container BuildForRavenDB()
        {
            return new CompositionRootBootstrap()
               .UseRavenDB()
               .UseCommands()
               .UseQueries()
               .Build();
        }

        public static Container BuildForInMemoryDB()
        {
            return new CompositionRootBootstrap()
               .UseInMemoryDB()
               .UseCommands()
               .UseQueries()
               .Build();
        }
    }
}
