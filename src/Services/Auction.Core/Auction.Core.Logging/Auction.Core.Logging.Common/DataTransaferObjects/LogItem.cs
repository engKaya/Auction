namespace Auction.Core.Logging.Common.Classes
{
    public class LogItem
    {
        private string _message = string.Empty;
        private object? _request;
        private object? _response;
        private Dictionary<string, object> _properties = new Dictionary<string, object>();
        private Exception? _exception;

        public LogItem(string message)
        {
            Message = message;
        }
        public LogItem(string message, object request)
        {
            Message = message;
            Request = request;
        }
        public LogItem(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }
        public LogItem(string message, object request, Exception exception)
        {
            Message = message;
            Request = request;
            Exception = exception;
        } 
        public LogItem(string message, object request, object response)
        {
            Message = message;
            Request = request;
            Response = response;
        }


        public string Message { get => _message; set => _message = value; }
        public object?    Request { get => _request; set => _request = value; }
        public object? Response { get => _response; set => _response = value; } 
        public Exception? Exception { get => _exception; set => _exception = value; }
        public Dictionary<string, object> Properties { get => _properties; }

        public LogItem AddProperties(string key, object prop)
        {
            Properties.Add(key, prop);
            return this;
        }

        public LogItem AddProperties(Dictionary<string, object> props)
        {
            foreach (var prop in props)
            {
                Properties.Add(prop.Key, prop.Value);
            }

            return this;
        }
    }
}
