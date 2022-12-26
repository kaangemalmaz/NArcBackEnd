using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Abstract
{
    public interface IUserService
    {
        void Add(AuthRegisterDto authDto);
        IResult Update(User user);
        IResult ChangePassword(UserChangePasswordDto userChangePasswordDto); //Password Change
        IResult Delete(User user);
        IDataResult<List<User>> GetList();
        User GetByEmail(string email);
        IDataResult<User> GetById(int id);
        
    }
}
