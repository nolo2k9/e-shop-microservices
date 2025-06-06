using MediatR;

namespace BuildingBlocks.CQRS;
//iCommand handler, no reponse
public interface ICommandHandler<in TCommand> 
 : ICommandHandler<TCommand, Unit>
 where TCommand : ICommand<Unit>
 {}
//iCommand handler, get response, not null
public interface ICommandHandler<in TCommand, TResponse>
   : IRequestHandler<TCommand, TResponse>
   where TCommand : ICommand<TResponse>
   where TResponse : notnull
{ }