using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICommandHandler<TCommand>
    {
        Task ExecuteAsync(TCommand command);
    }
}
