using DesafioFGV.Domain.Entities;
using DesafioFGV.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioFGV.Infrastructure.Persistence.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext db) : base(db)
    {
    }

    public override async Task<Order?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Include(e => e.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<Order?> GetByIdNoIncludeAsync(Guid id)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}

