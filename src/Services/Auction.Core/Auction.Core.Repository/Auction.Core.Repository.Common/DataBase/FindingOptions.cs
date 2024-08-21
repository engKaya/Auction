namespace Auction.Core.Repository.Common.Entity
{
    public class FindingOptions
    {
        public bool IsIgnoreAutoIncludes { get; set; } = true;
        public bool IsAsNoTracking { get; set; } = true;
    }
}
