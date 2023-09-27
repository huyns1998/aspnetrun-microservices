using Discount.Grpc.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.Grpc.BL
{
    public interface ICouponService
    {
        Task<IEnumerable<Coupon>> GetAll();
        Task<Coupon> GetByProductName(string productName);
        Task<Coupon> Add(Coupon coupon);
        public Task<Coupon> Update(Coupon coupon);
        public Task<Coupon> Delete(int id);
        public Task<Coupon> GetById(int id);
    }
}
