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
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name boş olamaz;");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email boş olamaz;");
            RuleFor(u => u.ImageUrl).NotEmpty().WithMessage("Image boş olamaz;");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Şifre boş olamaz;")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }
}
