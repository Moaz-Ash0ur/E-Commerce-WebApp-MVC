using ECommerceStore.Entities;

namespace ECommerceStore.BLL.Interface
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetAll();
        OrderItem? GetById(int id);
        void Create(OrderItem product);
        void Update(OrderItem product);
        void Delete(int id);
    }

}
