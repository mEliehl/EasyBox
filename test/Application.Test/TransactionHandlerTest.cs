using Application.Interfaces;
using Domain.Repositories;
using Infra.InMemoryDB.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test
{
    public class TransactionHandlerTest
    {
        readonly IUnitOfWork unitOfWork = new UnitOfWork();

        class AnyCommand
        {
            public int Cout { get; set; }
        }

        class AnyCommandDcorator : ICommandHandler<AnyCommand>
        {
            public async Task ExecuteAsync(AnyCommand command)
            {
                await Task.Factory.StartNew(() => command.Cout++);
            }            
        }

        class ExceptionCommandDcorator : ICommandHandler<AnyCommand>
        {
            public async Task ExecuteAsync(AnyCommand command)
            {
                await Task.Factory.StartNew(() => { throw new Exception(); });
            }
        }

        [Fact]
        public async void ShouldDecoratedTransactionHandlerAndSum()
        {
            var command = new AnyCommand();
            var decorator = new AnyCommandDcorator();
            var transactionHandler = new TransactionHandler<AnyCommand>(unitOfWork, decorator);
            await transactionHandler.ExecuteAsync(command);
            Assert.Equal(1, command.Cout);

        }

        [Fact]
        public async void ShouldDecoratedTransactionHandlerAndThrowException()
        {
            var command = new AnyCommand();
            var decorator = new ExceptionCommandDcorator();
            var transactionHandler = new TransactionHandler<AnyCommand>(unitOfWork, decorator);
            await Assert.ThrowsAsync<Exception>(() => transactionHandler.ExecuteAsync(command) );
        }
    }
}
