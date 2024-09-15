using Auction.Core.Repository.Common.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Auction.Core.Repository.Common.Context
{
    public abstract class BaseDbContext : DbContext  
    {
        public string _schemaName = string.Empty;
        public string _moduleCode = string.Empty; 
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            GetConfigureServiceModels(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public abstract void GetConfigureServiceModels(ModelBuilder modelBuilder);
        public abstract void SetSchemaName();
        public void SetSchemaName(string schemaName) => _schemaName = schemaName;
        public abstract void SetModuleCode();
        public void SetModuleCode(string moduleCode) => _moduleCode = moduleCode;
    }
}
