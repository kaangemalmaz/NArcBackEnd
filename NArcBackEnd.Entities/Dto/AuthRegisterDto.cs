using Microsoft.AspNetCore.Http;

namespace NArcBackEnd.Entities.Dto
{
    public class AuthRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image  { get; set; } //imageurl i kendimiz dolduracağımız için ona gerek yok burada!
    }
}
