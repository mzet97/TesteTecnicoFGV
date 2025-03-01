using DesafioFGV.Domain.Shared;
using DesafioFGV.Domain.Shared.Responses;
using System.Linq.Expressions;

namespace DesafioFGV.Domain.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task AddAsync(TEntity entity);

    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity?> GetByIdNoIncludeAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task UpdateAsync(TEntity entity);

    Task RemoveAsync(Guid id);
    Task DisableAsync(Guid id);
    Task ActiveAsync(Guid id);
    Task ActiveOrDisableAsync(Guid id, bool active);

    Task<BaseResultList<TEntity>> SearchAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int pageSize = 10, int page = 1);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}
