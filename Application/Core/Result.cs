namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public bool IsNull { get; set; }
        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value, IsNull = false };
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error, IsNull = false };
    }
}