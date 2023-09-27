using Discount.API.Entities;
using Discount.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.API.BL
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<Coupon> Add(Coupon coupon)
        {
            return await _couponRepository.Add(coupon);
        }

        public Task<Coupon> Delete(int id)
        {
            return _couponRepository.Delete(id);    
        }

        public async Task<IEnumerable<Coupon>> GetAll()
        {
            return await _couponRepository.GetAll();  
        }

        public async Task<Coupon> GetById(int id)
        {
            return await _couponRepository.GetById(id);   
        }

        public Task<Coupon> Update(Coupon coupon)
        {
            return _couponRepository.Update(coupon);
        }
    }
}
