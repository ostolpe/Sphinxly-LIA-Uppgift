using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Entities
{
    public class ProductEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
    }
}
