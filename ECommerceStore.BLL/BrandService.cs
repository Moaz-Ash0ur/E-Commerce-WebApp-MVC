using ECommerceStore.BLL.Interface;
using ECommerceStore.DAL.Repositories;
using ECommerceStore.Entities;

namespace ECommerceStore.BLL
{
    public class BrandService : IBrandService
    {
        private readonly IGenericRepository<Brand> _brandRepo;

        public BrandService(IGenericRepository<Brand> BrandRepo)
        {
            _brandRepo = BrandRepo;
        }


        public IEnumerable<Brand> GetAll() => _brandRepo.GetAll();

        public Brand? GetById(int id) => _brandRepo.GetById(id);

        public void Create(Brand Brand)
        {
            _brandRepo.Add(Brand);
            _brandRepo.Save();
        }

        public void Update(Brand Brand)
        {
            _brandRepo.Update(Brand);
            _brandRepo.Save();
        }

        public void Delete(int id)
        {
            var Brand = _brandRepo.GetById(id);
            if (Brand != null)
            {
                _brandRepo.Delete(Brand);
                _brandRepo.Save();
            }
        }
    
    
       


    }



}
