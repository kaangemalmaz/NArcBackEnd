using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim userOperationClaim);
        IResult Update(UserOperationClaim userOperationClaim);
        IResult Delete(UserOperationClaim userOperationClaim);
        IDataResult<List<UserOperationClaim>> GetList();
        IDataResult<UserOperationClaim> GetById(int id);
    }
}
