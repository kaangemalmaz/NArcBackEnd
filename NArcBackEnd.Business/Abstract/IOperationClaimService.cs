using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Abstract
{
    public interface IOperationClaimService
    {
        void Add(OperationClaim operationClaim);
    }
}
