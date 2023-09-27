using Discount.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.API.BL
{
    public interface ICouponService
    {
        Task<IEnumerable<Coupon>> GetAll();
        Task<Coupon> Add(Coupon coupon);
        public Task<Coupon> Update(Coupon coupon);
        public Task<Coupon> Delete(int id);
        public Task<Coupon> GetById(int id);
    }
}
