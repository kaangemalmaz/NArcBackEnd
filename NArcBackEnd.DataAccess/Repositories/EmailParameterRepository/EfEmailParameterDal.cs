using NArcBackEnd.Core.DataAccess.EntityFramework;
using NArcBackEnd.DataAccess.Concrete.EntityFramework;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.DataAccess.Repositories.EmailParameterRepository
{
    public class EfEmailParameterDal : EfEntityRepositoryBase<EmailParameter, BackEndContextDb>, IEmailParameterDal
    {

    }
}
