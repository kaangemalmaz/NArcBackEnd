using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Abstract
{
    public interface IOperationClaimService
    {
        IResult Add(OperationClaim operationClaim);
        IResult Update(OperationClaim operationClaim);
        IResult Delete(OperationClaim operationClaim);
        IDataResult<List<OperationClaim>> GetList();
        IDataResult<OperationClaim> GetById(int id);
    }
}
