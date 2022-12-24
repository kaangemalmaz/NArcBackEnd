using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        void add(UserOperationClaim userOperationClaim);
    }
}
