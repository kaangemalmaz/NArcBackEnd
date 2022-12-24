using NArcBackEnd.Core.Utilities.Result.Abstract;

namespace NArcBackEnd.Core.Utilities.Business
{
    public class BusinessRules
    {
        // tüm kontrolleri al.
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    // eğer hata olan varsa onu dön ve yapıdan çık!
                    return logic;
                }
            }

            //yoksa null dön.
            return null;
        }
    }
}
