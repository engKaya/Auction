namespace Auction.Core.Base.Common.Infastructure
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public BaseProcessCodes ProcessCode { get; set; } = BaseProcessCodes.Success; 
    }

    public class  BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; } = default;
    }
}
