namespace Auction.Core.Base.Common.Assembly
{
    public class CustomAssemblyPathInfo
    {
        public string Path { get; set; } = string.Empty;
        public string Name 
        {
            get  
            {
                var dllNameIndex = Path.LastIndexOf("\\");
                var dllName = Path.Substring(dllNameIndex + 1).Replace(".dll", "");
                return dllName;
            } 
        
        }

        public CustomAssemblyPathInfo(string path)
        {
            Path = path; 
        }
    }
}
