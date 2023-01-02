using FluentValidation;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Repositories.EmailParameterRepository.Validation.FluentValidation
{
    public class EmailParameterValidator : AbstractValidator<EmailParameter>
    {
        public EmailParameterValidator()
        {
            RuleFor(p => p.Smtp).NotEmpty().WithMessage("Smtp is required");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(p => p.Port).NotEmpty().WithMessage("Port is required");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(p => p.SSL).NotEmpty().WithMessage("SSL is required");
        }
    }
}
