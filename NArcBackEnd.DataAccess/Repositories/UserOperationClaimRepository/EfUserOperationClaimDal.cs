using NArcBackEnd.Core.DataAccess.EntityFramework;
using NArcBackEnd.DataAccess.Concrete.EntityFramework;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.DataAccess.Repositories.UserOperationClaimRepository
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, BackEndContextDb>, IUserOperationClaimDal
    {
    }
}
