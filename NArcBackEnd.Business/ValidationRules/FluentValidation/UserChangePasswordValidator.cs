using FluentValidation;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.ValidationRules.FluentValidation
{
    public class UserChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(u => u.NewPassword).NotEmpty().WithMessage("Şifre boş olamaz")
               .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
               .MaximumLength(15).WithMessage("Şifre en az 6 karakter olmalıdır")
               .Matches("[A-Z]").WithMessage("Şifreniz en az 1 adet büyük harf içermelidir.")
               .Matches("[a-z]").WithMessage("Şifreniz en az 1 adet küçük harf içermelidir.")
               .Matches("[0-9]").WithMessage("Şifreniz en az 1 adet sayi içermelidir.");
        }
    }
}
