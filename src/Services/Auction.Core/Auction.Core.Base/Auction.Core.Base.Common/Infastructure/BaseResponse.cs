using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Auction.Core.Base.Common.Infastructure
{
    public class BaseResponse
    {
        private string _message = string.Empty;
        public bool IsSuccess { get => ProcessCode.Code == BaseProcessCodes.Success.Code; }
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message))
                    return $"{ProcessCode.Message}({ProcessCode.Code})";

                return _message;
            }
            set { _message = value; }
        }
        public BaseProcessCodes ProcessCode { get; set; } = BaseProcessCodes.Success;

        [JsonIgnore]
        public Exception? Exception { get; private set; } = default;
        public string ExceptionType { get => Exception?.GetType().Name ?? string.Empty; }
        public string ExceptionLocation { get; private set; } = string.Empty;
        public string ExceptionMessage { get => Exception?.Message ?? string.Empty; }
        public string InnerExceptionMessage { get => Exception?.InnerException?.Message ?? string.Empty; }
        public string InnerExceptionType { get => Exception?.InnerException?.GetType().Name ?? string.Empty; }
        public string InnerExceptionLocation { get => Exception?.InnerException?.StackTrace ?? string.Empty; }
        public void SetException(Exception exception, [CallerFilePath] string filepath = "", [CallerMemberName] string member = "", [CallerLineNumber] int number = 0)
        {
            Exception = exception;
            ExceptionLocation = $"{filepath}|{member}|{number}";
            ProcessCode = BaseProcessCodes.InternalServerError;
        }
    }

    public class BaseResponse<T> : BaseResponse where T : class, new()
    {

        public T? Data { get; set; } = new();
    }
}
