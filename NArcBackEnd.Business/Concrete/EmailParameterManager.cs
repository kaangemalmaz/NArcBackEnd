using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Business.Constans;
using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Core.Utilities.Result.Concrete;
using NArcBackEnd.DataAccess.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Concrete
{
    public class EmailParameterManager : IEmailParameterService
    {
        private readonly IEmailParameterDal _emailParameterDal;

        public EmailParameterManager(IEmailParameterDal emailParameterDal)
        {
            _emailParameterDal = emailParameterDal;
        }

        public IResult Add(EmailParameter emailParameter)
        {
            //kontroller.
            _emailParameterDal.Add(emailParameter);
            return new SuccessResult(Messages.AddedEmailParameter);
        }

        public IResult Delete(EmailParameter emailParameter)
        {
            _emailParameterDal.Delete(emailParameter);
            return new SuccessResult(Messages.DeletedEmailParameter);
        }

        public IDataResult<EmailParameter> GetById(int id)
        {
            return new SuccessDataResult<EmailParameter>(_emailParameterDal.Get(o => o.Id == id));
        }

        public IDataResult<List<EmailParameter>> GetList()
        {
            return new SuccessDataResult<List<EmailParameter>>(_emailParameterDal.GetAll());
        }

        public IResult Update(EmailParameter emailParameter)
        {

            _emailParameterDal.Update(emailParameter);
            return new SuccessResult(Messages.UpdatedEmailParameter);
        }
    }
}
