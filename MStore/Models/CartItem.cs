using System.Text.Json;

namespace MStore.Models
{
    public class CartItem
    {       
       //represent the item in ShoppingCart dsiplay for customer in browser and save in cookies
      public int ProductId { get; set; }
      public string ProductName { get; set; } = "";
      public string ImageName { get; set; } = "";
      public decimal UnitPrice { get; set; }
      public int Quantity { get; set; }
      public decimal Total { get; set; }

    }


    public interface ICartService
    {
        List<CartItem> GetCartItems();
        void RemoveFromCart(int productId);
        void ClearCart();
        void SaveCartItems(List<CartItem> items);
    }


    public class CookieCartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _cookieName = "Cart";

    public CookieCartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

     private HttpContext HttpContext => _httpContextAccessor.HttpContext!;
    

      public List<CartItem> GetCartItems()
    {    
       var cookie = HttpContext.Request.Cookies[_cookieName];

        if (string.IsNullOrEmpty(cookie))
            return new List<CartItem>();

             return JsonSerializer.Deserialize<List<CartItem>>(cookie) ?? new List<CartItem>();
        }
           
      public void RemoveFromCart(int productId)
      {
          var items = GetCartItems();
          items = items.Where(i => i.ProductId != productId).ToList();
          SaveCartItems(items);
      }
      
      public void ClearCart()
      {
          HttpContext.Response.Cookies.Delete(_cookieName);
      }
      
      public void SaveCartItems(List<CartItem> items)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                //  Secure = true,
                // SameSite = SameSiteMode.Lax,
                HttpOnly = true
            };

            var json = JsonSerializer.Serialize(items);
            HttpContext.Response.Cookies.Append(_cookieName, json, options);
        }



    }



}

