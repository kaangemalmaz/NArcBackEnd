using FluentValidation;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.ValidationRules.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            //RuleFor(p => p.UserId).NotEmpty().WithMessage("Kullanici seçimi zorunludur")
            //                      .GreaterThan(0).WithMessage("Kullanici seçimi zorunludur");

            //RuleFor(p => p.OperationClaimId).NotEmpty().WithMessage("Yetki seçimi zorunludur")
            //                            .GreaterThan(0).WithMessage("Kullanici seçimi zorunludur");

            RuleFor(p => p.UserId).Must(IsIdValid).WithMessage("Kullanici seçimi zorunludur");
            RuleFor(p => p.OperationClaimId).Must(IsIdValid).WithMessage("Kullanici seçimi zorunludur");

            
        }

        private bool IsIdValid(int id)
        {
            if(id>0 && id != null)
            {
                return true;
            }
            return false;
        }
    }
}
