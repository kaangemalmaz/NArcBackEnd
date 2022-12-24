using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Abstract
{
    public interface IUserService
    {
        void Add(AuthRegisterDto authDto);
        List<User> GetList();

        User GetByEmail(string email);
    }
}
