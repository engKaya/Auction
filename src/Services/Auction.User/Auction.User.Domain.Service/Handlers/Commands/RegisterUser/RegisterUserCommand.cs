using Auction.Core.Base.Common.Infastructure;
using MediatR;

namespace Auction.User.Domain.Service.Handlers.Commands.RegisterUser
{
    public class RegisterUserCommand : BaseRequest, IRequest
    {
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _phoneNumber = string.Empty;
         
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        } 
    }
}
