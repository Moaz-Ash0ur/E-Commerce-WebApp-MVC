namespace ECommerceStore.Entities
{
    public class Order
    {
        public enum PaymentMethod { CashOnDelivery, PayPal, CreditCard }
        public enum PaymentStatus { Pending, Accepted, Canceled}
        public enum OrderStatus { Created, Accepted, Shipped, Delivered, Canceled , Deleted }



        public int Id { get; set; }
        public string ClientId { get; set; }
        public string DeliveryAddress { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public PaymentStatus paymentStatus { get; set; }
        public string PaymentDetails { get; set; }
        public decimal ShippingFee { get; set; }
        public OrderStatus orderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }


        //Navigation
        public ApplicationUser Client { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new();
    }





}
