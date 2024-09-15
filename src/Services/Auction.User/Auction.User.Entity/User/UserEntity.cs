using Auction.Core.Repository.Common.DataBase;

namespace Auction.User.Entity.User
{
    public class UserEntity : BaseEntity
    {
        private int _id;
        private string _email;

        public int Id { get => _id; set => _id = value; }
        public string Email { get => _email; set => _email = value; } 
        public List<UserPhoneEntity> UserPhone { get; set; } = new List<UserPhoneEntity>(); 
    }
}
