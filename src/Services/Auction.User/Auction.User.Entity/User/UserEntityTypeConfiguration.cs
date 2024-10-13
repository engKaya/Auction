using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.User.Entity.User
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("USER");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Id).HasColumnName("Id");    
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Password).HasColumnName("Password").HasColumnType<string>("varchar(250)");
            builder.Property(x => x.PasswordSalt).HasColumnName("PasswordSalt").HasColumnType<string>("varchar(250)");
            builder.HasMany(x => x.UserPhone).WithOne().HasForeignKey(x => x.UserId);
        }
    }
}
