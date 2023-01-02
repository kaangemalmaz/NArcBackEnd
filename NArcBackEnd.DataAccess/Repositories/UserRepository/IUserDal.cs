using NArcBackEnd.Core.DataAccess;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.DataAccess.Repositories.UserRepository
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetUserOperationClaims(int userId);
    }
}
