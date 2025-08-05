using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Entities.Views
{ 
        public class OrderDetailsVw
        {
        public int OrderId { get; set; }
        public string DeliveryAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }

        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClientAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    } 

}
