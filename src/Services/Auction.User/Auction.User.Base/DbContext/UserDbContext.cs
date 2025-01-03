﻿using Auction.Core.Repository.Common.Context;
using Auction.User.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace Auction.User.Base.DbContext
{
    public class UserDbContext : BaseDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {  

        }
         

        public DbSet<UserPhoneEntity> Users { get; set; }
        public DbSet<UserPhoneEntity> UserPhones { get; set; }
        public override void GetConfigureServiceModels(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserPhoneEntityConfiguration());
        }

        public override void SetModuleCode()
        {
            SetModuleCode("AUCTION_USER");
        }

        public override void SetSchemaName()
        {
            SetSchemaName("AUCTION_USER");
        }
    }
}
