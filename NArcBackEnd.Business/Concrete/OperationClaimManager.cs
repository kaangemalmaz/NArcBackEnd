using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Business.Aspects.Secured;
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
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            //kontroller.
            IResult result = BusinessRules.Run(IsNameAvaible(operationClaim.Name));
            if (result != null) return result;


            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.AddedOperationClaim);
        }

        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.DeletedUser);
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(o => o.Id == id));
        }


        [SecuredAspect()]
        //[SecuredAspect("Admin")]
        //[SecuredAspect("Admin, GetList")]

        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameAvaible(operationClaim.Name));
            if (result != null) return result;

            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.UpdatedOperationClaim);
        }


        private IResult IsNameAvaible(string name)
        {
            var result = _operationClaimDal.Get(o => o.Name.ToLower() == name.ToLower());
            if (result != null)
            {
                return new ErrorResult(Messages.ExitsNameOperationClaim);
            }
            return new SuccessResult();
        }
    }
}
