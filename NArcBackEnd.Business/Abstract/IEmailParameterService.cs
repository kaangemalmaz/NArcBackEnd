using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Business.Abstract
{
    public interface IEmailParameterService
    {
        IResult Add(EmailParameter emailParameter);
        IResult Update(EmailParameter emailParameter);
        IResult Delete(EmailParameter emailParameter);
        IDataResult<List<EmailParameter>> GetList();
        IDataResult<EmailParameter> GetById(int id);

    }
}
