using FluentValidation;

namespace NArcBackEnd.Core.CrossCuttingConcerns.Validation
{
    // cross cutting concerns core katmanına yazılacak tüm yapıları etkileyecek yapılardır.
    // genele olarak tüm yapılarda kullanılabilecek yapıları içerir.
    // o yüzden generic hale getirilir ve static olarak eklenir genellikle.
    public class ValidationTools
    {
        /* bunun karşılığı aşağısı.
         UserValidator validationRules = new UserValidator();
            ValidationResult result = validationRules.Validate(authRegisterDto);
            if (!result.IsValid)
                return "Zorunlu alanları doldurunuz.";
         */

        // validator fluent validationdan türemiş hangi kontrol mekanizmasını çalıştırmak istediğin
        // entity ise o kontrol mekanizmasında hangi entity'nin kontrol edileceğidir.
        // validator içinde validate metodunu bulundurur.
        public static void Validate(IValidator validator, object entity)
        {

            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
