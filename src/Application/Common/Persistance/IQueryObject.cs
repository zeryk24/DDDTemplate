using System.Linq.Expressions;

namespace Application.Common.Persistance;

public interface IQueryObject<TAggregate> where TAggregate : class, new()
{
    IQueryObject<TAggregate> Filter(Expression<Func<TAggregate, bool>> predicate);
    IQueryObject<TAggregate> OrderBy<TKey>(Expression<Func<TAggregate, TKey>> selector, bool ascending = true);
    IQueryObject<TAggregate> Page(int page, int pageSize);

    Task<IEnumerable<TAggregate>> ExecuteAsync();
}

