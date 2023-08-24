using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Entities
{
    public class Product
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Category { get; set; }
        [MaxLength(200)]
        public string Summary { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string ImageFile { get; set; }
        public decimal Price { get; set; }

    }
}
