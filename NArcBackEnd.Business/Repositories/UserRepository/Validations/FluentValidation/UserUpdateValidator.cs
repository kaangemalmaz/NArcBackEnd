using FluentValidation;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Repositories.UserRepository.Validations.FluentValidation
{
    public class UserUpdateValidator : AbstractValidator<User>
    {
        public UserUpdateValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name boş olamaz");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email boş olamaz");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
            RuleFor(u => u.ImageUrl).NotEmpty().WithMessage("Image boş olamaz");
        }
    }
}
