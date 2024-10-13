using Auction.Core.Repository.Common.DataBase;

namespace Auction.User.Entity.User
{
    public class UserPhoneEntity : BaseEntity
    {
        private int _id;
        private int _userId;
        private string _phone = string.Empty;
        private bool _valid;

        public int Id { get => _id; set => _id = value; } 
        public int UserId { get => _userId; set => _userId = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public bool Valid { get => _valid; set => _valid = value; }
        public UserEntity User { get; set; } = null!;
    }
}
