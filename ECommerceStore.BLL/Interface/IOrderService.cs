using ECommerceStore.Entities;
using ECommerceStore.Entities.Views;
using Microsoft.EntityFrameworkCore;

namespace ECommerceStore.BLL.Interface
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Create(Order order);
        void Update(Order order);
        void Delete(int id);
        public IEnumerable<Order> GetAllInclude(params string[] agers);
        public IEnumerable<OrderDetailsVw> GetOrderDetails();

        public (IQueryable<Order>, int) PaginateOrders(int page, int pageSize);
        public IEnumerable<Order> GetAllAsQueryable();




    }

}
