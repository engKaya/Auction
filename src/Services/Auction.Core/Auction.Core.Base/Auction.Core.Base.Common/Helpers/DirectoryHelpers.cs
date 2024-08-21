namespace Auction.Core.Base.Common.Helpers
{
    public static class DirectoryHelpers
    {
        public static DirectoryInfo GetDirectoryParent(this DirectoryInfo directory, int level = 1)
        {  
            for (int i = 0; i < level; i++)
            {
                directory = directory.Parent;
            }
            return directory; 
        }

        public static IEnumerable<string> GetFilesWithRecursive(this DirectoryInfo directory, string searchPattern, bool includeSubdirectories = true)
        {
            var searchOption = includeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly; 
            return Directory.GetFiles(directory.FullName, searchPattern, searchOption);
        }
    }
}
