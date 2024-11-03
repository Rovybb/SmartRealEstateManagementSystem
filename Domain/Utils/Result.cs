namespace Domain.Utils
{
    public class Result<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        protected Result(bool isSuccess, T data, string errorMessage)
        {
            Data = data;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, data, null);
        }

        public static Result<T> Fail(string errorMessage)
        {
            return new Result<T>(false, default!, errorMessage);
        }
    }
}
