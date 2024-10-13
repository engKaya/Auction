using Auction.Core.Base.Common.Extensions;
using Auction.Core.Repository.Common.DataBase;
using Auction.User.Entity.Helpers;

namespace Auction.User.Entity.User
{
    public class UserEntity : BaseEntity
    {
        private int _id;
        private string _email = string.Empty; 
        private string _password = string.Empty;
        private string _passwordSalt = string.Empty;

        public int Id { get => _id; set => _id = value; }
        public string Email { get => _email; set => _email = value; } 
        public List<UserPhoneEntity> UserPhone { get; set; } = new List<UserPhoneEntity>(); 
        public string Password { get => _password; set  => _password = value; }
        public string PasswordSalt { get => _passwordSalt; set => _passwordSalt = value; }

        public void SetPassword(string plainPassword) 
        {
            _password = PasswordHelper.HashPasword(plainPassword, out byte[] salt);
            _passwordSalt = salt.ToHexString();
        }

        public bool VerifyPassword(string plainPassword) 
        {
            var salt = _passwordSalt.StringToByteArray();
            return PasswordHelper.VerifyPassword(plainPassword, _password, salt);
        }

    }
}
