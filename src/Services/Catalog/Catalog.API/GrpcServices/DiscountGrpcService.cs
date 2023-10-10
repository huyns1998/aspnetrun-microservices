using System.Threading.Tasks;
using Catalog.API.Protos;
namespace Catalog.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoservice.DiscountProtoserviceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoservice.DiscountProtoserviceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
