using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Querys.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var filteredOrders = orders
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName.Value)
            .ToList();

        return new GetOrdersByNameResult(filteredOrders.ToOrderDtoList());

    }
}