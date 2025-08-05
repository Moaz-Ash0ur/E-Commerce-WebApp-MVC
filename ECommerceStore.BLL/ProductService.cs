using ECommerceStore.BLL.Interface;
using ECommerceStore.DAL.Repositories;
using ECommerceStore.Entities;
using ECommerceStore.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace ECommerceStore.BLL
{
    public partial class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductDetailsVM> _productVwRepo;

        public ProductService(IGenericRepository<Product> productRepo,IGenericRepository<ProductDetailsVM> productVMRepo)
        {
            _productRepo = productRepo;
            _productVwRepo = productVMRepo;
        }

        public IEnumerable<ProductDetailsVM> GetAllWithCategory()
        {
            return _productVwRepo.GetAll();
        }


        public (IQueryable<ProductDetailsVM>, int) PaginateProducts(int page, int pageSize,string? searchValue = "")
        {

            var query = _productVwRepo.GetAllAsQueryable();


            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = Serach(searchValue);
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            var paginated = query
                .OrderByDescending(p => p.ProductID)
                .Paginate(page, pageSize);

            return (paginated, totalPages);
        }


        public IQueryable<ProductDetailsVM> Serach(string search)
        {
             var allProductDetails = _productVwRepo.GetAllAsQueryable();

             var resultOfSearch = allProductDetails
             .Where(p => p.Category == search || 
             p.Brand == search || 
             p.Name == search);      
            
            return resultOfSearch;
        }



        public IEnumerable<Product> GetAll()
        {
            return _productRepo.GetAll();
        }

        public Product? GetById(int id) => _productRepo.GetById(id);

        public void Create(Product product)
        {
            _productRepo.Add(product);
            _productRepo.Save();
        }

        public void Update(Product product)
        {
            _productRepo.Update(product);
            _productRepo.Save();
        }

        public void Delete(int id)
        {
            var product = _productRepo.GetById(id);
            if (product != null)
            {
                 product.IsDeleted = true;
                _productRepo.Save();
            }
        }

        public ProductDetailsVM? GetDetailsById(int id)
        {
            return _productVwRepo.GetAll().SingleOrDefault(p => p.ProductID == id);          
        }





    }


}
