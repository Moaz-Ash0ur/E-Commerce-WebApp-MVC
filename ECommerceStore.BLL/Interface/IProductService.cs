using ECommerceStore.Entities;
using ECommerceStore.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.BLL.Interface
{

    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Create(Product product);
        void Update(Product product);
        void Delete(int id);

        IEnumerable<ProductDetailsVM> GetAllWithCategory();
        ProductDetailsVM? GetDetailsById(int id);
        public (IQueryable<ProductDetailsVM>, int) PaginateProducts(int page, int pagesize, string? searchValue = "");
        public IQueryable<ProductDetailsVM> Serach(string search);
    }

}
