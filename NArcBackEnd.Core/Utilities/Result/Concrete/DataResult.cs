using NArcBackEnd.Core.Utilities.Result.Abstract;

namespace NArcBackEnd.Core.Utilities.Result.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(bool success, T data) : base(success)
        {
            Data = data;
        }

        public DataResult(bool success, string message, T data) : base(success, message)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
