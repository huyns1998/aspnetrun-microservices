using Discount.Grpc.Dao;
using Discount.Grpc.Data;
using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
    public class CouponRepository : ACrudDao<Coupon>, ICouponRepository
    {
        public CouponRepository(DiscountDbContext context) : base(context)
        {
        }
    }
}
