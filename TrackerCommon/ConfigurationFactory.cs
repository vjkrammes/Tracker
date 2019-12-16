using System.IO;

using Microsoft.Extensions.Configuration;

namespace TrackerCommon
{
    public static class ConfigurationFactory
    {
        public static IConfiguration Create(bool optional = true) => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Constants.ConfigurationFilename, optional: optional)
            .Build();
    }
}
