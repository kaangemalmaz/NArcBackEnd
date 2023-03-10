using NArcBackEnd.Business.Repositories.UserRepository.Constants;
using NArcBackEnd.Business.Repositories.UserRepository.Validations.FluentValidation;
using NArcBackEnd.Business.Utilities.File;
using NArcBackEnd.Core.Aspects.Caching;
using NArcBackEnd.Core.Aspects.Transaction;
using NArcBackEnd.Core.Aspects.Validation;
using NArcBackEnd.Core.Utilities.Hashing;
using NArcBackEnd.Core.Utilities.Result.Abstract;
using NArcBackEnd.Core.Utilities.Result.Concrete;
using NArcBackEnd.DataAccess.Repositories.UserRepository;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.Business.Repositories.UserRepository
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

        [RemoveCacheAspect("IUserService.GetList")] // cache - 6 //regex sayesinde bunu buluyor.
        public async void Add(AuthRegisterDto authRegisterDto)
        {
            string fileName = _fileService.FileSaveToServer(authRegisterDto.Image, "./Content/img/");
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

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(UserMessages.DeletedUser);
        }

        [CacheAspect(60)] // cache - 5
        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        [ValidationAspect(typeof(UserChangePasswordValidator))]
        [TransactionAspect]
        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto) //Password Change
        {

            var user = _userDal.Get(u => u.Id == userChangePasswordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt);
            if (!result) return new ErrorResult(UserMessages.WrongPassword);


            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Update(user);
            return new SuccessResult(UserMessages.PasswordChanged);

        }

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            return _userDal.GetUserOperationClaims(userId);
        }
    }
}
