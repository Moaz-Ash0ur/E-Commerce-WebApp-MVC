using ECommerceStore.BLL.Interface;
using ECommerceStore.DAL.Repositories;
using ECommerceStore.Entities;

namespace ECommerceStore.BLL
{
    public class OrderItemService : IOrderItemService
    {

        private readonly IGenericRepository<OrderItem> _orderItemRepo;

        public OrderItemService(IGenericRepository<OrderItem> orderItemRepo)
        {
            _orderItemRepo = orderItemRepo;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _orderItemRepo.GetAll();
        }

        public OrderItem? GetById(int id) => _orderItemRepo.GetById(id);

        public void Create(OrderItem order)
        {
            _orderItemRepo.Add(order);
            _orderItemRepo.Save();
        }

        public void Update(OrderItem order)
        {
            _orderItemRepo.Update(order);
            _orderItemRepo.Save();
        }

        public void Delete(int id)
        {
            var product = _orderItemRepo.GetById(id);
            if (product != null)
            {
                _orderItemRepo.Save();
            }
        }

    }


}
