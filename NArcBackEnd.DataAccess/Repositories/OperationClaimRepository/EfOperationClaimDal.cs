using NArcBackEnd.Core.DataAccess.EntityFramework;
using NArcBackEnd.DataAccess.Concrete.EntityFramework;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.DataAccess.Repositories.OperationClaimRepository
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, BackEndContextDb>, IOperationClaimDal
    {
    }
}
