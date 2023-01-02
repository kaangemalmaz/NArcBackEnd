using NArcBackEnd.Core.DataAccess.EntityFramework;
using NArcBackEnd.DataAccess.Concrete.EntityFramework;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.DataAccess.Repositories.UserRepository
{
    public class EfUserDal : EfEntityRepositoryBase<User, BackEndContextDb>, IUserDal
    {
        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            using (var context = new BackEndContextDb())
            {
                var result = from userOperationClaim in context.UserOperationClaims.Where(p => p.UserId == userId)
                             join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             };
                return result.OrderBy(p => p.Name).ToList();
            }
        }
    }
}
