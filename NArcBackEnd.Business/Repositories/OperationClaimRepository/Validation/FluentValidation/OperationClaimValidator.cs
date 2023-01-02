using FluentValidation;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Repositories.OperationClaimRepository.Validation.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(o => o.Name).NotEmpty().WithMessage("Yetki adı boş olamaz!");
        }
    }
}
