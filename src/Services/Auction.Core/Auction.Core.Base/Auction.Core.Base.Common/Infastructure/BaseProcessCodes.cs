namespace Auction.Core.Base.Common.Infastructure
{
    public class BaseProcessCodes
    {
        private string _code = string.Empty;
        private string _message = string.Empty;
        private Exception? _exception = null;
        private string _exceptionMessage = string.Empty;
        private string _exceptionStackTrace = string.Empty;

        public string Code { get => _code; set => _code = value; }
        public string Message { get => _message; set => _message = value; }

        public BaseProcessCodes()
        {
                
        }
        public BaseProcessCodes(string code, string message)
        {
            _code = code;
            _message = message;
        }
         
        public void SetException(Exception exception)
        {
            _exception = exception;
            _exceptionMessage = exception.Message;
            _exceptionStackTrace = exception.StackTrace;
        }
        
        public static BaseProcessCodes Success => new BaseProcessCodes("000", "Success");
        public static BaseProcessCodes BusinessError => new BaseProcessCodes("001", "Business Error");
        public static BaseProcessCodes InternalServerError => new BaseProcessCodes("500", "Internal Server Error"); 

    }
}
