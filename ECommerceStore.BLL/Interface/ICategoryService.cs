using ECommerceStore.Entities;

namespace ECommerceStore.BLL.Interface
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(int id);
    }



}
