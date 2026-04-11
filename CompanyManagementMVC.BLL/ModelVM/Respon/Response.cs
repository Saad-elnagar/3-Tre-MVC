namespace CompanyManagementMVC.BLL.ModelVM.Response
{
    public class Response<TResult>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public TResult? Data { get; private set; }

        public static Response<TResult> Success(TResult data, string message = "Success")
            => new() { IsSuccess = true, Data = data, Message = message };

        public static Response<TResult> Failure(string message)
            => new() { IsSuccess = false, Data = default, Message = message };
    }
}