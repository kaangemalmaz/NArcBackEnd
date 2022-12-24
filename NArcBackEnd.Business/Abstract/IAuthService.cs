using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Abstract
{
    public interface IAuthService
    {
        IResult Register(AuthRegisterDto authRegisterDto);
        string Login(AuthLoginDto authLoginDto);
    }
}
