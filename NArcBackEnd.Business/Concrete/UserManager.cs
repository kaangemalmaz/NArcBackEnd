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
        private readonly IFileService _fileService;

        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public async void Add(AuthRegisterDto authRegisterDto)
        {
            string fileName = _fileService.FileSave(authRegisterDto.Image, "./Content/img/");
            User user = CreateUserInfo(authRegisterDto, fileName);
            _userDal.Add(user);
        }

        private static User CreateUserInfo(AuthRegisterDto authRegisterDto, string fileName)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(authRegisterDto.Password, out passwordHash, out passwordSalt);

            User user = new User()
            {
                Id = 0,
                Email = authRegisterDto.Email,
                ImageUrl = fileName,
                Name = authRegisterDto.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            return user;
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
