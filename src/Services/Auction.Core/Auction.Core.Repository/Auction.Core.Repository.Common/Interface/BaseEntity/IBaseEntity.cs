﻿namespace Auction.Core.Repository.Common.Interface.BaseEntity
{
    public interface IBaseEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        string RequestId { get; set; }
        bool IsDeleted { get; set; } 
    }
}
