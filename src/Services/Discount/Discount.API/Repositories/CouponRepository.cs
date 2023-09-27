using Discount.API.Dao;
using Discount.API.Data;
using Discount.API.Entities;

namespace Discount.API.Repositories
{
    public class CouponRepository : ACrudDao<Coupon>, ICouponRepository
    {
        public CouponRepository(DiscountDbContext context) : base(context)
        {
        }
    }
}
