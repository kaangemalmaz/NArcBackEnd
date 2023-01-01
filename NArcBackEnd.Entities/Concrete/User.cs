
namespace NArcBackEnd.Entities.Concrete
{
    public class User 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public byte[] PasswordHash { get; set; } //binary == byte[]
        public byte[] PasswordSalt { get; set; }
    }
}
