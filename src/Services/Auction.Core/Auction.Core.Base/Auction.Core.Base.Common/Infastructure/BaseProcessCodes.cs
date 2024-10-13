namespace Auction.Core.Base.Common.Infastructure
{
    public class BaseProcessCodes
    {
        private string _code = string.Empty;
        private string _message = string.Empty; 
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
          
        
        public static BaseProcessCodes Success => new BaseProcessCodes("000", "Success");
        public static BaseProcessCodes BusinessError => new BaseProcessCodes("001", "Business Error");
        public static BaseProcessCodes PasswordOrMailInCorrect => new BaseProcessCodes("002", "Password or email incorrect!");
        public static BaseProcessCodes InternalServerError => new BaseProcessCodes("500", "Internal Server Error"); 

    }
}
