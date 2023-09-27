namespace Discount.API.Dtos
{
    public class CouponCreateDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
