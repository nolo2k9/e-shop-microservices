using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;
public record GetOrdersRequest(PaginatedRequest PaginatedRequest);
public record GetOrderResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginatedRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request));
            var response = result.Adapt<GetOrderResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");
    }
}