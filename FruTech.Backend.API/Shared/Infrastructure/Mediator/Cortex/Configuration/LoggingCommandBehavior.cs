using Cortex.Mediator.Commands;

namespace FruTech.Backend.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
/// <summary>
///  Logs command execution details
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public class LoggingCommandBehavior<TCommand>
: ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    public async Task Handle(
        TCommand command,
        CommandHandlerDelegate next,
        CancellationToken ct)
    {
        await next();
    }
    
}