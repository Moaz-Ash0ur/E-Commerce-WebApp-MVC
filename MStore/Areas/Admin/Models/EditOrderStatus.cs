using static ECommerceStore.Entities.Order;

namespace MStore.Areas.Admin.Models
{
    public class EditOrderStatus
    {
        public int Id { get; set; }

        public PaymentStatus paymentStatus { get; set; }

        public OrderStatus orderStatus { get; set; }
    }

}
