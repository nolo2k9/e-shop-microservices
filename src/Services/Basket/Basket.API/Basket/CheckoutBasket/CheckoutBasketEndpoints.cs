
namespace Basket.API.Basket.CheckoutBasket;

public record BasketCheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record BasketCheckoutResponse(bool IsSuccess);

public class CheckoutBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/chckout", async (BasketCheckoutBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutBasketCommand>();
            var result = await sender.Send(command);
            
            var resposne = result.Adapt<BasketCheckoutResponse>();
            
            return Results.Ok(resposne);
            
        })
        .WithName("CheckoutBasket")
        .Produces<BasketCheckoutResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket");
    }
}
    
