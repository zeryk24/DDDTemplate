using Application.Common.Attributes;
using Application.Common.Persistance;
using Application.Common.Pipelines;
using ErrorOr;
using MediatR;
using System.Transactions;

namespace Application.Common.Behaviors;

[RegisterOpenBehavior(typeof(UnitOfWorkBehavior<,>))]
public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IUnitOfWorkPipeline 
    where TResponse : IErrorOr
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        using var transactionScope = new TransactionScope();
        var response = await next();

        if (response.IsError)
            return response;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        transactionScope.Complete();

        return response;
    }
}
