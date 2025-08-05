using System.ComponentModel.DataAnnotations;

namespace ECommerceStore.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation
        public ICollection<Product> Products { get; set; }
    }


}
