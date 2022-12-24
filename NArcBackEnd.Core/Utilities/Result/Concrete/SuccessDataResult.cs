namespace NArcBackEnd.Core.Utilities.Result.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(true, data)
        {
        }

        public SuccessDataResult(string message, T data) : base(true, message, data)
        {
        }
    }
}
