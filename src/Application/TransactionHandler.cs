using Application.Interfaces;
using Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Application
{
    public class TransactionHandler<TCommand> : ICommandHandler<TCommand>
    {
        readonly IUnitOfWork unitOfWork;
        readonly ICommandHandler<TCommand> decorated;

        public TransactionHandler(IUnitOfWork unitOfWork,
            ICommandHandler<TCommand> decorated)
        {
            this.unitOfWork = unitOfWork;
            this.decorated = decorated;
        }

        public async Task ExecuteAsync(TCommand command)
        {
            try
            {
                await decorated.ExecuteAsync(command);
                await unitOfWork.Commit();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
