using NArcBackEnd.Business.Abstract;
using NArcBackEnd.DataAccess.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }


        public void Add(OperationClaim operationClaim)
        {
            //kontroller.
            _operationClaimDal.Add(operationClaim);
        }
    }
}
