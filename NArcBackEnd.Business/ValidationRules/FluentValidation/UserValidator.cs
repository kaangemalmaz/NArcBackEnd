using FluentValidation;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.ValidationRules.FluentValidation
{
    // FLUENT VALIDATION
    public class UserValidator : AbstractValidator<AuthRegisterDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name boş olamaz");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email boş olamaz");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
            RuleFor(u => u.ImageUrl).NotEmpty().WithMessage("Image boş olamaz");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
                .MaximumLength(15).WithMessage("Şifre en az 6 karakter olmalıdır")
                .Matches("[A-Z]").WithMessage("Şifreniz en az 1 adet büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifreniz en az 1 adet küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifreniz en az 1 adet sayi içermelidir.");
                //.Matches("[*a-zA-Z0-9]").WithMessage("Şifreniz en az 1 adet özel karakter içermelidir.");

        }
    }
}
