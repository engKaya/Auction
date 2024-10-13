using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.User.Entity.User
{
    public class UserPhoneEntityConfiguration : IEntityTypeConfiguration<UserPhoneEntity>
    { 
        public void Configure(EntityTypeBuilder<UserPhoneEntity> builder)
        {
            builder.ToTable("USER_PHONE");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.Valid).HasColumnName("Valid"); 
            builder.HasOne(x => x.User).WithMany(x => x.UserPhone).HasForeignKey(x => x.UserId);
        }
    }
}
