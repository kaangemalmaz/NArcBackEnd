using FluentValidation.Results;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Business.ValidationRules.FluentValidation;
using NArcBackEnd.Core.CrossCuttingConcerns.Validation;
using NArcBackEnd.Core.Utilities.Business;
using NArcBackEnd.Core.Utilities.Hashing;
using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Core.Utilities.Result.Concrete;
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

        public IResult Register(AuthRegisterDto authRegisterDto)
        {
            int imgSize = 2;

            UserValidator rules = new UserValidator();
            ValidationTools.Validate(rules, authRegisterDto);

            //business rules configuration
            var result = BusinessRules.Run(
                CheckIfEmailExists(authRegisterDto.Email),
                CheckIfImageSizeOneMbAbove(imgSize)
            );

            if (!result.Success) return result;
            //if (result != null) return result;

            _userService.Add(authRegisterDto);
            return new SuccessResult("İşlem başarı ile tamamlanmıştır.");

        }


        private IResult CheckIfEmailExists(string email)
        {
            User user = _userService.GetByEmail(email);
            if (user != null) return new ErrorResult("Bu email adresi kullanılmaktadır.");
            return new SuccessResult();
        }

        private IResult CheckIfImageSizeOneMbAbove(int imageSize)
        {
            if (imageSize > 1)
            {
                return new ErrorResult("Dosya boyutu 1mbdan büyük olamaz");
            }
            return new SuccessResult();
        }

    }
}
