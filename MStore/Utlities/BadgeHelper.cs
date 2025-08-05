namespace MStore.Utlities
{
    public static class BadgeHelper
    {
        public static string GetPaymentBadge(this string status)
        {
            return status?.ToLower() switch
            {
                "pending" => "bg-warning text-dark",
                "accepted" => "bg-success",
                "canceled" => "bg-secondary",
                _ => "bg-danger"
            };
        }

        public static string GetOrderBadge(this string status)
        {
            return status?.ToLower() switch
            {
                "created" => "bg-warning text-dark",
                "accepted" => "bg-success",
                "canceled" => "bg-secondary",
                "shipped" => "bg-primary",
                "delivered" => "bg-success",
                "returned" => "bg-info text-dark",
                "deleted" => "bg-dark",
                _ => "bg-danger"
            };
        }

        public static string GetRoleBadge(this IEnumerable<string> roles)
        {
            if (roles == null) return "bg-secondary";

            if (roles.Contains("admin", StringComparer.OrdinalIgnoreCase))
                return "bg-danger";
            else if (roles.Contains("customer", StringComparer.OrdinalIgnoreCase))
                return "bg-info text-dark";

            return "bg-secondary";
        }



    }

}
