using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.Core.Utilities.Security.JWT
{
    public interface ITokenHandler
    {
        Token CreateToken(User user, List<OperationClaim> operationClaims); //bana tokeni dönecek yapıdır.
    }
}
