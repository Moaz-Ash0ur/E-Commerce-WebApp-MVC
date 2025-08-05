using ECommerceStore.BLL.Interface;
using ECommerceStore.DAL.Repositories;
using ECommerceStore.Entities;
using ECommerceStore.Entities.Views;
using Microsoft.EntityFrameworkCore;

namespace ECommerceStore.BLL
{
    public class OrderService : IOrderService
    {

        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<OrderDetailsVw> _orderDetailsRepo;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<OrderDetailsVw> orderDetailsRepo)
        {
            _orderRepo = orderRepo;
            _orderDetailsRepo = orderDetailsRepo;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepo.GetAll();
        }

        public IEnumerable<Order> GetAllInclude(params string[] agers)
        {
            return _orderRepo.GetAllInclude(agers);
        }

        public IEnumerable<Order> GetAllAsQueryable()
        {
            return _orderRepo.GetAllAsQueryable()
            .Include(o => o.Client)
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Product);
        }


        public Order? GetById(int id) => _orderRepo.GetById(id);

        public void Create(Order order)
        {
            _orderRepo.Add(order);
            _orderRepo.Save();
        }

        public void Update(Order order)
        {
            _orderRepo.Update(order);
            _orderRepo.Save();
        }

        public void Delete(int id)
        {
            var product = _orderRepo.GetById(id);
            if (product != null)
            {
                _orderRepo.Save();
            }
        }

        public IEnumerable<OrderDetailsVw> GetOrderDetails()
        {
           return _orderDetailsRepo.GetAll();
        }

        public (IQueryable<Order>, int) PaginateOrders(int page, int pageSize)
        {

            var query = _orderRepo.GetAllInclude("Client","Items");

         
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            var paginated = query
                .OrderByDescending(p => p.CreatedAt)
                .Paginate(page, pageSize);

            return (paginated, totalPages);
        }




    }


}
