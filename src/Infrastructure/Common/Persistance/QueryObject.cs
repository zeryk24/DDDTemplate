using Application.Common.Persistance;
using System.Linq.Expressions;

namespace Infrastructure.Common.Persistance;

public abstract class QueryObject<TAggregate> : IQueryObject<TAggregate> where TAggregate : class, new()
{
    protected IQueryable<TAggregate> _query;

    protected QueryObject(IQueryable<TAggregate> query)
    {
        _query = query;
    }

    public IQueryObject<TAggregate> Filter(Expression<Func<TAggregate, bool>> predicate)
    {
        _query = _query.Where(predicate);
        return this;
    }

    public IQueryObject<TAggregate> Page(int page, int pageSize)
    {
        _query = _query.Skip((page - 1) * pageSize).Take(pageSize);
        return this;
    }

    public IQueryObject<TAggregate> OrderBy<TKey>(Expression<Func<TAggregate, TKey>> selector, bool ascending = true)
    {
        _query = ascending switch
        {
            true => _query.OrderBy(selector),
            false => _query.OrderByDescending(selector)
        };
        return this;
    }

    public abstract Task<IEnumerable<TAggregate>> ExecuteAsync();
}