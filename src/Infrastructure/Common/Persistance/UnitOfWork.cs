using Application.Common.Persistance;
using Domain.Common.Attributes;

namespace Infrastructure.Common.Persistance;

[Register<IUnitOfWork>]
public class UnitOfWork : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
