using System.Text;

namespace NArcBackEnd.Core.Utilities.Hashing
{
    public class HashingHelper
    {


        public static void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) //sınıfın içini boş bırakırsan rastgele key oluşturur. yoksa senin verdiğin yapıdan bir key oluşturur.
            {
                passwordSalt = hmac.Key; //burası da keyidir.
                //var aa = Encoding.UTF8.GetBytes(password); aldığı yapıdaki byteları 8 bitlik decimale kodluyor demektir.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Encoding.utf8.getbytes şifreyi byte a çevirir. computeHash şifreyi hashler.
            }
        }


        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //mantık gelen şifreyi aynı salt ile yeniden şifrele(hashle) ve sonra hashteki şifre ile uyuşuyor mu kontrol et!
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))  //password salt bu olacak şekilde compute yap demek.
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //hashle verilen salta göre.

                for (int i = 0; i < computeHash.Length; i++) // onun tüm bytelarını al.
                {
                    if (computeHash[i] != passwordHash[i]) // eğer tüm bytelar uyuşmuyorsa false dön. Hata var.
                    {
                        return false;
                    }
                }

                return true; //uyuşuyorsa true dön. Hata yok!
            }
        }




    }
}
