using Castle.DynamicProxy;
using FluentValidation;
using NArcBackEnd.Core.CrossCuttingConcerns.Validation;
using NArcBackEnd.Core.Utilities.Interceptors;

namespace NArcBackEnd.Core.Aspects.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gelen type eğer IValidatordan değilse hata ver!
            {
                throw new Exception("Hatalı validation sınıfı");
            }

            _validatorType = validatorType;
        }


        //metodu ne zaman çalıştırmak istediğimizi söyleyelim hemne!
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // instance üret.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //validatorun istediği entity tipini yakaladık. Fluent validationdaki entity
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //metoda gelen argümanları yakala! validationda olanlara eşitse
            foreach (var entity in entities)
            {
                ValidationTools.Validate(validator, entity);
            }
        }
    }
}
