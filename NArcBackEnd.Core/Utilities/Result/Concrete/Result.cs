using NArcBackEnd.Core.Utilities.Result.Abstract;

namespace NArcBackEnd.Core.Utilities.Result.Concrete
{
    public class Result : IResult
    {
        //constructor da set edilebiliyor dışarıdan ayrıyetten set edilemez hale getiriliyor.

        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public bool Success { get; }
        public string Message { get;}
    }
}
