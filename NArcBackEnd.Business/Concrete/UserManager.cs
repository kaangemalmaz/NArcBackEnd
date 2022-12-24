using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Core.Utilities.Hashing;
using NArcBackEnd.DataAccess.Abstract;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(AuthRegisterDto authRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(authRegisterDto.Password, out passwordHash, out passwordSalt);

            User user = new User()
            {
                Id = 0,
                Email = authRegisterDto.Email,
                ImageUrl = "",
                Name = authRegisterDto.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userDal.Add(user);
        }

        public User GetByEmail(string email)
        {

            return _userDal.Get(u => u.Email == email); 
        }

        public List<User> GetList()
        {
            return _userDal.GetAll();
        }
    }
}
