using ECommerceStore.BLL.Interface;
using ECommerceStore.DAL.Repositories;
using ECommerceStore.Entities;

namespace ECommerceStore.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepo;

        public CategoryService(IGenericRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IEnumerable<Category> GetAll() => _categoryRepo.GetAll();

        public Category? GetById(int id) => _categoryRepo.GetById(id);

        public void Create(Category category)
        {
            _categoryRepo.Add(category);
            _categoryRepo.Save();
        }

        public void Update(Category category)
        {
            _categoryRepo.Update(category);
            _categoryRepo.Save();
        }

        public void Delete(int id)
        {
            var category = _categoryRepo.GetById(id);
            if (category != null)
            {
                _categoryRepo.Delete(category);
                _categoryRepo.Save();
            }
        }
    
    
       


    }



}
