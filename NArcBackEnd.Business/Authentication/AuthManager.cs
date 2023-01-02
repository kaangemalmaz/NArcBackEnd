using FluentValidation.Results;
using NArcBackEnd.Business.Repositories.UserRepository;
using NArcBackEnd.Business.Repositories.UserRepository.Validations.FluentValidation;
using NArcBackEnd.Core.Aspects.Validation;
using NArcBackEnd.Core.CrossCuttingConcerns.Validation;
using NArcBackEnd.Core.Utilities.Business;
using NArcBackEnd.Core.Utilities.Hashing;
using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Core.Utilities.Result.Concrete;
using NArcBackEnd.Core.Utilities.Security.JWT;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthManager(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public IDataResult<Token> Login(AuthLoginDto authLoginDto)
        {
            //Cross cutting concers - Uygulamayı dikine kesmek.
            //AOP

            var user = _userService.GetByEmail(authLoginDto.Email);
            var passwordResult = HashingHelper.VerifyPasswordHash(authLoginDto.Password, user.PasswordHash, user.PasswordSalt);

            if (passwordResult)
            {
                List<OperationClaim> operationClaims = _userService.GetUserOperationClaims(user.Id);
                Token token = new Token();
                token = _tokenHandler.CreateToken(user, operationClaims);
                return new SuccessDataResult<Token>(token);
            }

            return new ErrorDataResult<Token>("Kullanici maili yada şifre bilgisi yanlıştır.");

        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Register(AuthRegisterDto authRegisterDto)
        {
            //business rules configuration
            var result = BusinessRules.Run(
                CheckIfEmailExists(authRegisterDto.Email),
                CheckIfImageExtensionsAllow(authRegisterDto.Image.FileName),
                CheckIfImageSizeOneMbAbove(authRegisterDto.Image.Length)
            );

            if (result != null) return result;


            _userService.Add(authRegisterDto);
            return new SuccessResult("İşlem başarı ile tamamlanmıştır.");

        }


        private IResult CheckIfEmailExists(string email)
        {
            User user = _userService.GetByEmail(email);
            if (user != null) return new ErrorResult("Bu email adresi kullanılmaktadır.");
            return new SuccessResult();
        }

        private IResult CheckIfImageSizeOneMbAbove(long imageSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imageSize * 0.000001); // IFormFile dosyayı byte olarak verir mb'a çeviriyoruz.
            if (imgMbSize > 1)
            {
                return new ErrorResult("Dosya boyutu 1mbdan büyük olamaz");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageExtensionsAllow(string fileName)
        {
            var extension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();

            List<string> AllowFileExtensions = new List<string> { "jpg", "jpeg", "gif", "png" };

            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Eklediğiniz resim jpg, jpeg, gif, png tiplerinden biri olmalıdır");
            }
            return new SuccessResult();
        }

    }
}
