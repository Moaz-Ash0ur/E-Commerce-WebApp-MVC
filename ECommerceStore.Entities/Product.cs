using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [MaxLength(100)]

        public string ImageName { get; set; }
        [Display(Name = "Category")]

        public int CategoryId { get; set; }
        [Display(Name = "Brand")]

        public int BrandId { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Navigation
        public Category Category { get; set; }

        public Brand Brand { get; set; }

    }

   






}
