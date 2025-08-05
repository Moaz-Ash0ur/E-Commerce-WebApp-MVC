using ECommerceStore.Entities;

namespace ECommerceStore.BLL.Interface
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAll();
        Brand? GetById(int id);
        void Create(Brand category);
        void Update(Brand category);
        void Delete(int id);
    }



}
