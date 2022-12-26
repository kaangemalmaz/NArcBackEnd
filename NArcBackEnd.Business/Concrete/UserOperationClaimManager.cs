using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Business.Constans;
using NArcBackEnd.Business.ValidationRules.FluentValidation;
using NArcBackEnd.Core.Aspects.Validation;
using NArcBackEnd.Core.Utilities.Business;
using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Core.Utilities.Result.Concrete;
using NArcBackEnd.DataAccess.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserService _userService;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService, IUserService userService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
            _userService = userService;
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(
                IsUserExits(userOperationClaim.UserId),
                IsOperationClaimExits(userOperationClaim.OperationClaimId),
                IsOperationSetAvailable(userOperationClaim)
                );
            if (result != null) return result;

            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.AddedUserOperationClaim);
        }

        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.DeletedUserOperationClaim);
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(u => u.Id == id));
        }

        public IDataResult<List<UserOperationClaim>> GetList()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll());
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            //kontroller
            IResult result = BusinessRules.Run(
                IsUserExits(userOperationClaim.UserId),
                IsOperationClaimExits(userOperationClaim.OperationClaimId),
                IsOperationSetAvailable(userOperationClaim)
                );
            if (result != null) return result;

            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UpdatedUserOperationClaim);
        }

        private IResult IsOperationSetAvailable(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.Get(p => p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
            if (result != null) return new ErrorResult(Messages.NotAvailableUserOperationClaim);
            return new SuccessResult();
        }

        private IResult IsOperationClaimExits(int operationClaimId)
        {
            var result = _operationClaimService.GetById(operationClaimId);
            if (result.Data == null) return new ErrorResult(Messages.NotExitsOperationClaim);
            return new SuccessResult();
        }

        private IResult IsUserExits(int userId)
        {
            var result = _userService.GetById(userId);
            if (result.Data == null) return new ErrorResult(Messages.NotExitsUser);
            return new SuccessResult();
        }

    }
}
