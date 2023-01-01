using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Core.Utilities.Security.JWT;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Abstract
{
    public interface IAuthService
    {
        IResult Register(AuthRegisterDto authRegisterDto);
        IDataResult<Token> Login(AuthLoginDto authLoginDto);
    }
}
