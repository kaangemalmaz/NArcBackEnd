using NArcBackEnd.Business.Abstract;
using NArcBackEnd.DataAccess.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public void add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
        }
    }
}
