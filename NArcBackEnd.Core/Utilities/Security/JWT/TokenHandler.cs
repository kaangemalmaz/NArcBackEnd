using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NArcBackEnd.Core.Extensions;
using NArcBackEnd.Entities.Concrete;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NArcBackEnd.Core.Utilities.Security.JWT
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //https://www.gencayyildiz.com/blog/asp-net-core-3-1-ile-token-bazli-kimlik-dogrulamasi-ve-refresh-token-kullanimijwt/
            //https://www.gencayyildiz.com/blog/asp-net-core-angular-7-web-api-token-authentication-kullanimi/

            Token token = new Token();
            token.Expiration = DateTime.Now.AddMinutes(60); //60 dk sonra token biter

            // security key simetriğini alalım 
            // yani şifreyi byte olarak alıp simetrik olarak üretiyoruz.
            // debugla!
            // var aa = Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]);
            // SecurityKey yerine symmetric kullanılmasının sebebi byte olarak alabildiği için.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            // şifrelenmiş kimliği oluştur.
            // hmac 256 ile security keyi şifreliyoruz.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // token ayarları
            // jwt bearerin istediği ayarlar.
            // token içeriğini belirliyor.
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now, //token ne zaman geçerli olacak!
                signingCredentials: signingCredentials,
                claims: SetClaims(user, operationClaims)
                );


            // token oluşturucudan bir örnek al!
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //token üret
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            //refresh token üret.
            token.RefreshToken = CreateRefreshToken();

            return token;

        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create()) //random sayi üretir.
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            //https://www.gencayyildiz.com/blog/asp-net-core-identity-claim-bazli-kimlik-dogrulama-xvii/
            var claims = new List<Claim>();
            claims.AddName(user.Name);
            claims.AddRoles(operationClaims.Select(p=>p.Name).ToArray());
            // array olarak aldığımız için arraye çevrilir.
            // sadece name gelmesi gerektiği için select ile içinden sadece name seçilir. id gelmesin diye!

            return claims;
        }
    }
}
