namespace NArcBackEnd.Core.Utilities.Security.JWT
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; } //tokenin ne zaman biteceğini gösterir.
        public string RefreshToken { get; set; } // expiration süresi bittiğinde tokeni yenilemeyi sağlar. yeniden token oluşturur.
    }
}
