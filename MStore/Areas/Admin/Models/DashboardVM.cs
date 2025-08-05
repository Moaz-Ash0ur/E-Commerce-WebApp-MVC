
namespace MStore.Areas.Admin.Models
{
    public class DashboardVM
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalSales { get; set; }
        public List<string> SalesDates { get; set; } = new();
        public List<decimal> SalesTotals { get; set; } = new();

      
    }
}
