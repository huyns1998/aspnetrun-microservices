using AutoMapper;
using Discount.Grpc.BL;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountGrpcService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;

        public DiscountGrpcService(ICouponService couponService, IMapper mapper)
        {
            _couponService = couponService;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponService.GetByProductName(request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName = {request.ProductName} could not be found"));
            }

            Console.WriteLine($"Discount is retrieved from ProductName {coupon.ProductName}, Amount {coupon.Amount}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _couponService.Add(coupon);

            Console.WriteLine($"Discount is successfully created. ProductName ${coupon.ProductName}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _couponService.Update(coupon);

            Console.WriteLine($"Discount is successfully updated. ProductName ${coupon.ProductName}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _couponService.Delete(request.Coupon.Id);

            var response = new DeleteDiscountResponse()
            {
                Success = deleted != null
            };

            if (deleted != null)
            {
                Console.WriteLine($"Discount is successfully deleted. ProductName ${request.Coupon.ProductName}");
            }
            else
            {
                Console.WriteLine($"Discount is fail deleted. ProductName ${request.Coupon.ProductName}");
            }

            return response;
        }
    }
}
    