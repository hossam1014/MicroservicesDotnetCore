using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.gRPC.Protos;

namespace Basket.API.gRPCServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }


        public async Task<CouponModel> GetDiscount(string productName)
        {
            var result = await _discountProtoService
                    .GetDiscountAsync(new GetDiscountRequest{ProductName = productName});

            return result;
        }
    }
}