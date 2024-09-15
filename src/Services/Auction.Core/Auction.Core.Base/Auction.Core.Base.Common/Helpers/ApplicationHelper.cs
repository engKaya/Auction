using Auction.Core.Base.Common.Enums;

namespace Auction.Core.Base.Common.Helpers
{
    public static class ApplicationHelper
    {
        public static EnvironmentType GetEnvironmentType()
        {
            var environment = Environment.GetEnvironmentVariable(EnviromentConstants.ENVIRONMENT);
            return environment switch
            {
                "Development" => EnvironmentType.Development,
                "Test" => EnvironmentType.Test,
                "Production" => EnvironmentType.Production,
                _ => EnvironmentType.Development
            };
        }
    }
}
