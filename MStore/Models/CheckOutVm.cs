namespace MStore.Models
{
    public class CheckOutVm
    {
        public List<CartItem> lslCartItem { get; set; } = new List<CartItem>();
        public string DeliveryAddress { get; set; }
        public string paymentMethod { get; set; }

    }
}
