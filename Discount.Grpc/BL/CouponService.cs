using Discount.Grpc.Entities;
using Discount.Grpc.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.Grpc.BL
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

        public async Task<Coupon> GetByProductName(string productName)
        {
            return await _couponRepository.GetSingleByCondition(x => x.ProductName.Equals(productName));
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
