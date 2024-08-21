using Auction.Core.Base.Common.Assembly;
using Auction.Core.Base.Common.Helpers;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Auction.Core.Host.Service.Helpers
{
    public static class AssemblyBuilderHelper
    { 
        private static List<Assembly> _assembly = [];
        public static List<Assembly> GetAssembliesFromDll(ILogger logger ) 
        {
            if (_assembly.Count > 0)
                return _assembly;

            var hostPath = AppContext.BaseDirectory;
            logger.LogInformation($"Path: {hostPath}");
            var directory = new DirectoryInfo(hostPath);
            var entryPath = directory.GetDirectoryParent(6);

            var foundedFiles = entryPath.GetFilesWithRecursive("Auction*.dll").Where(x => !x.Contains("\\obj\\")).ToList();
            List<CustomAssemblyPathInfo> dllInfos = new List<CustomAssemblyPathInfo>();

            foreach (var item in foundedFiles)
            {
                var assemblyPath = new CustomAssemblyPathInfo(item);
                if (!dllInfos.Exists(x=>x.Name == assemblyPath.Name) && assemblyPath.Name!= "Auction.Core.Host.Presentation")
                    dllInfos.Add(assemblyPath);
            }

             List<Assembly> assemblies = [];
             
             if (directory.Exists)
             { 
                 logger.LogWarning($"{dllInfos.Count()} files found!.");
                 foreach (var dll in dllInfos)
                 {
                     var ass = Assembly.LoadFrom(dll.Path);
                     assemblies.Add(ass);
                 }
             }

            _assembly = assemblies;
            return _assembly;

        }
    }
}
