using NArcBackEnd.Core.DataAccess.EntityFramework;
using NArcBackEnd.DataAccess.Abstract;
using NArcBackEnd.DataAccess.Concrete.EntityFramework.Context;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, BackEndContextDb>, IUserDal
    {
    }
}
