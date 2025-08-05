namespace MStore.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; }
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public List<string> Roles { get; set; }
        public DateTime CreatedAt { get; set; }




    }

}
