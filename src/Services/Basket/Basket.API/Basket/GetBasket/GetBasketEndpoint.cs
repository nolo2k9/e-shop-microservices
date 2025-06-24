namespace Basket.API.Basket.GetBasket;
//Records used for data transfer objects (DTOs) provide built-in value equality and concise syntax.
public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //sender is an instance of ISender (usually from MediatR), used to send queries/commands.
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            //Maps the result to a response DTO
            var response = result.Adapt<GetBasketResponse>();
            
            return Results.Ok(response);
            
        })
        .WithName("GetProductById")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}