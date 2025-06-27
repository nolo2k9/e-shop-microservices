using Grpc.Core;
using Discount.Grpc;
using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) 
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                coupon = new Coupon {ProductName = "No Discount", Amount = 0, Description = "No Discount"}; 
            
            logger.LogInformation("Discount is found for ProductName: {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            
            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            
            logger.LogInformation("Discount is created for ProductName: {productName}", coupon.ProductName);
            
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            
            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            
            logger.LogInformation("Discount is updated for ProductName: {productName}", coupon.ProductName);
            
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Cant delete Discount"));
            
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is deleted for ProductName: {productName}", coupon.ProductName);
            
            return new DeleteDiscountResponse {Success = true};
        }
    }
