using Microsoft.AspNetCore.Identity;
using System;


namespace ECommerceStore.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; } = "";
        public DateTime CreatedAt { get; set; }



    }
}
