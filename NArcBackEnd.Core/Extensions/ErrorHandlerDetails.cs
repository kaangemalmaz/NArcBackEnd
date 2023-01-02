using FluentValidation.Results;
using Newtonsoft.Json;

namespace NArcBackEnd.Core.Extensions
{
    // Exception Handler - 1
    public class ErrorHandlerDetails 
    {
        // hata mesajları için genel bir yapının oluşturulması

        public string Messages { get; set; }
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ValidationErrorDetails : ErrorHandlerDetails
    {
        // validation hatalarında 1 den fazla hata gelebiliyor onun için genel bir yapının oluşturulması
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
