namespace ProductCategory.Areas.Identity.Models
{
    public class Usuario
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int IdRol { get; set; }

    }
}
