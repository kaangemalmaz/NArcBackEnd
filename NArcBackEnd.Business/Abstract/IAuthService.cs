using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Abstract
{
    public interface IAuthService
    {
        string Register(AuthRegisterDto authRegisterDto);
        string Login(AuthLoginDto authLoginDto);
    }
}
