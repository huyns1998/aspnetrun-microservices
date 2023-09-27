using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Discount.Grpc.Entities
{
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(24)]
        public string ProductName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}
