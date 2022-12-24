using FluentValidation.Results;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Business.ValidationRules.FluentValidation;
using NArcBackEnd.Core.Utilities.Hashing;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public string Login(AuthLoginDto authLoginDto)
        {
            //Cross cutting concers - Uygulamayı dikine kesmek.
            //AOP

            var user = _userService.GetByEmail(authLoginDto.Email);
            var passwordResult = HashingHelper.VerifyPasswordHash(authLoginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (passwordResult)
            {
                return "Kullanici girişi başarılıdır.";
            }
            return "Kullanici bilgileri hatali";
            
        }

        public string Register(AuthRegisterDto authRegisterDto)
        {
            UserValidator validationRules = new UserValidator();
            ValidationResult result = validationRules.Validate(authRegisterDto);
            if (!result.IsValid)
                return "Zorunlu alanları doldurunuz.";

            _userService.Add(authRegisterDto);
            return "işlem başarılı";
        }
    }
}
